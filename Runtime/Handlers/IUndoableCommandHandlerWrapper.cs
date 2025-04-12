using System.Threading.Tasks;

/// <summary>
/// Interface for handling undoable commands.
/// </summary>
public interface IUndoableCommandHandlerWrapper : ICommandHandlerWrapper
{
    /// <summary>
    /// Asynchronously undos the command using the command handler.
    /// </summary>
    /// <param name="command">The command to undo.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UndoAsync(IUndoableCommand command);
}