using System.Collections.Generic;

/// <summary>
/// Interface for a command stack.
/// </summary>
/// <typeparam name="TCommand">The type of command.</typeparam>
public interface ICommandRecorder<TCommand>
    where TCommand : ICommand
{
    /// <summary>
    /// Number of commands in the stack.
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Push a command onto the stack.
    /// </summary>
    /// <param name="command">The command to push.</param>
    void Push(TCommand command);

    /// <summary>
    /// Pop a command from the stack.
    /// </summary>
    /// <returns>The popped command.</returns>
    TCommand Pop();

    /// <summary>
    /// Clear the stack.
    /// </summary>
    void Clear();

    /// <summary>
    /// Get the history of commands in order.
    /// </summary>
    /// <returns>An enumerable of commands in order.</returns>
    IEnumerable<TCommand> GetHistoryInOrder();
}
