using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class CommandDispatcher : ICommandDispatcher
{
    private readonly Dictionary<Type, ICommandHandlerWrapper> _handlers = new();

    public void Register<TCommand>(ICommandHandler<TCommand> handler) where TCommand : ICommand
    {
        _handlers.Add(typeof(TCommand), new CommandHandlerWrapper<TCommand>(handler));
    }

    private ICommandHandlerWrapper GetHandler<TCommand>() where TCommand : ICommand
    {
        if (_handlers.TryGetValue(typeof(TCommand), out var handler))
            return handler;

        throw new InvalidOperationException($"Handler not registered for {typeof(TCommand)}");
    }

    public Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        => GetHandler<TCommand>().ExecuteAsync(command);

    public Task<bool> ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand
        => GetHandler<TCommand>().ValidateAsync(command);
}
