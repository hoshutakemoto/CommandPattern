using System.Threading.Tasks;

public abstract class UndoableCommand<TCommand> : Command<TCommand>, IUndoableCommand
    where TCommand : UndoableCommand<TCommand>
{
    public Task UndoWithAsync(IUndoableCommandDispatcher dispatcher)
        => dispatcher.UndoAsync((TCommand)this);
}