using System.Threading.Tasks;

public sealed class UndoableCommandHandlerWrapper<TCommand> : IUndoableCommandHandlerWrapper
    where TCommand : IUndoableCommand
{
    private readonly IUndoableCommandHandler<TCommand> _inner;

    public UndoableCommandHandlerWrapper(IUndoableCommandHandler<TCommand> inner)
    {
        _inner = inner;
    }

    public Task ExecuteAsync(ICommand command) =>
        _inner.ExecuteAsync((TCommand)command);

    public Task UndoAsync(IUndoableCommand command) =>
        _inner.UndoAsync((TCommand)command);

    public Task<bool> ValidateAsync(ICommand command) =>
        _inner.ValidateAsync((TCommand)command);
}