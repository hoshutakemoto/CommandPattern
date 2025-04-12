using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

public sealed class UndoableCommandManager : IUndoableCommandManager
{
    private readonly IUndoableCommandDispatcher _dispatcher;
    private readonly ICommandRecorder<IUndoableCommand> _undoRecorder;
    private readonly ICommandRecorder<IUndoableCommand> _redoRecorder;
    private readonly ILogger _logger;
    private readonly ConcurrentQueue<IUndoableCommand> _commandQueue = new();
    private readonly SemaphoreSlim _queueLock = new(1, 1);

    public int HistoryCount => _undoRecorder.Count;
    public bool CanUndo => _undoRecorder.Count > 0;
    public bool CanRedo => _redoRecorder.Count > 0;
    public int UndoCount => _undoRecorder.Count;
    public int RedoCount => _redoRecorder.Count;

    public UndoableCommandManager(IUndoableCommandDispatcher dispatcher,
        ICommandRecorder<IUndoableCommand> undoRecorder,
        ICommandRecorder<IUndoableCommand> redoRecorder,
        ILogger logger)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
        _undoRecorder = undoRecorder ?? throw new ArgumentNullException(nameof(undoRecorder));
        _redoRecorder = redoRecorder ?? throw new ArgumentNullException(nameof(redoRecorder));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task ExecuteAsync(IUndoableCommand command)
    {
        var isValid = await command.ValidateWithAsync(_dispatcher);
        if (!isValid)
        {
            _logger.Warning($"Command validation failed: {command.CommandName}");
            return;
        }

        try
        {
            await command.ExecuteWithAsync(_dispatcher);
            _undoRecorder.Push(command);
            _redoRecorder.Clear(); // Clear redo commands after new command
        }
        catch (Exception ex)
        {
            _logger.Error($"Command execution failed: {command.CommandName}", ex);
        }
    }

    public void QueueCommand(IUndoableCommand command)
    {
        if (command == null) throw new ArgumentNullException(nameof(command));
        _commandQueue.Enqueue(command);
        ProcessQueueAsync().ConfigureAwait(false);
    }

    private async Task ProcessQueueAsync()
    {
        if (!await _queueLock.WaitAsync(0)) return;

        try
        {
            while (_commandQueue.TryDequeue(out var command))
            {
                await ExecuteAsync(command);
            }
        }
        finally
        {
            _queueLock.Release();
        }
    }

    public async Task UndoAsync()
    {
        if (!CanUndo)
            throw new InvalidOperationException("No commands to undo.");

        var command = _undoRecorder.Pop();

        try
        {
            await command.UndoWithAsync(_dispatcher);
            _redoRecorder.Push(command);
        }
        catch (Exception ex)
        {
            _undoRecorder.Push(command); // Push back to undo stack if undo fails
            _logger.Error($"Command undo failed: {command.CommandName}", ex);
        }
    }

    public async Task RedoAsync()
    {
        if (!CanRedo)
            throw new InvalidOperationException("No commands to redo.");

        var command = _redoRecorder.Pop();

        try
        {
            await command.ExecuteWithAsync(_dispatcher);
            _undoRecorder.Push(command);
        }
        catch (Exception ex)
        {
            _redoRecorder.Push(command); // Push back to redo stack if redo fails
            _logger.Error($"Command redo failed: {command.CommandName}", ex);
        }
    }

    public string SerializeHistory(ICommandSerializer serializer)
    {
        return serializer.Serialize(_undoRecorder.GetHistoryInOrder());
    }
}
