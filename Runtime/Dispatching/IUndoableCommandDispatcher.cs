using System.Threading.Tasks;

/// <summary>
/// Interface for dispatching commands.
/// This interface provides methods to execute, undo, and validate commands.
/// </summary>
public interface IUndoableCommandDispatcher : ICommandDispatcher
{
    /// <summary>
    /// Asynchronously undoes a command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to undo.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UndoAsync<TCommand>(TCommand command) where TCommand : IUndoableCommand;
}