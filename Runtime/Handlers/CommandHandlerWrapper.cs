using System.Threading.Tasks;

public sealed class CommandHandlerWrapper<TCommand> : ICommandHandlerWrapper
    where TCommand : ICommand
{
    private readonly ICommandHandler<TCommand> _inner;

    public CommandHandlerWrapper(ICommandHandler<TCommand> inner)
    {
        _inner = inner;
    }

    public Task ExecuteAsync(ICommand command) =>
        _inner.ExecuteAsync((TCommand)command);

    public Task<bool> ValidateAsync(ICommand command) =>
        _inner.ValidateAsync((TCommand)command);
}