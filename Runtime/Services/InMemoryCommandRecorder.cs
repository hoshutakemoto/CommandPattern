using System.Collections.Generic;
using System.Linq;

public sealed class InMemoryCommandRecorder<TCommand> : ICommandRecorder<TCommand>
    where TCommand : ICommand
{
    private readonly Stack<TCommand> _commands = new();

    public int Count => _commands.Count;

    public void Clear() => _commands.Clear();

    public IEnumerable<TCommand> GetHistoryInOrder() => _commands.Reverse();

    public TCommand Pop() => _commands.Pop();

    public void Push(TCommand command) => _commands.Push(command);
}