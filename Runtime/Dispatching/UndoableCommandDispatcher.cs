using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UndoableCommandDispatcher : IUndoableCommandDispatcher
{
    private readonly Dictionary<Type, IUndoableCommandHandlerWrapper> _handlers = new();

    public void Register<TCommand>(IUndoableCommandHandler<TCommand> handler) where TCommand : IUndoableCommand
    {
        _handlers.Add(typeof(TCommand), new UndoableCommandHandlerWrapper<TCommand>(handler));
    }

    private IUndoableCommandHandlerWrapper GetHandler<TCommand>() where TCommand : ICommand
    {
        if (_handlers.TryGetValue(typeof(TCommand), out var handler))
            return handler;

        throw new InvalidOperationException($"Handler not registered for {typeof(TCommand)}");
    }

    public Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand
        => GetHandler<TCommand>().ExecuteAsync(command);

    public Task UndoAsync<TCommand>(TCommand command) where TCommand : IUndoableCommand
        => GetHandler<TCommand>().UndoAsync(command);

    public Task<bool> ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand
        => GetHandler<TCommand>().ValidateAsync(command);
}
