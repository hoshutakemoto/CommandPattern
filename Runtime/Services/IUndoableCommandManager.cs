using System.Threading.Tasks;

/// <summary>
/// Interface for managing command history.
/// This interface provides methods to execute, undo, and redo commands.
/// </summary>
public interface IUndoableCommandManager
{
    /// <summary>
    /// Checks if there are any commands that can be undone.
    /// </summary>
    bool CanUndo { get; }

    /// <summary>
    /// Checks if there are any commands that can be redone.
    /// </summary>
    bool CanRedo { get; }

    /// <summary>
    /// Gets the number of commands that can be undone.
    /// </summary>
    int UndoCount { get; }

    /// <summary>
    /// Gets the number of commands that can be redone.
    /// </summary>
    int RedoCount { get; }

    /// <summary>
    /// Asynchronously executes a command and adds it to the history.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(IUndoableCommand command);

    /// <summary>
    /// Queues a command for execution.
    /// This method is typically used for commands that are not executed immediately.
    /// </summary>
    /// <param name="command">The command to queue.</param>
    void QueueCommand(IUndoableCommand command);

    /// <summary>
    /// Asynchronously undoes the last executed command.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task UndoAsync();

    /// <summary>
    /// Asynchronously redoes the last undone command.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    public Task RedoAsync();

    /// <summary>
    /// Serializes the command history using the provided serializer.
    /// </summary>
    /// <param name="serializer">The serializer to use for serialization.</param>
    /// <returns>A string representing the serialized command history.</returns>
    string SerializeHistory(ICommandSerializer serializer);
}