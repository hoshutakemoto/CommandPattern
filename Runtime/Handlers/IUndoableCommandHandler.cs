using System.Threading.Tasks;

/// <summary>
/// Interface for handling undoable commands.
/// This interface extends the ICommandHandler interface to include
/// functionality for undoing commands.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface IUndoableCommandHandler<TCommand> : ICommandHandler<TCommand>
    where TCommand : IUndoableCommand
{
    /// <summary>
    /// Asynchronously undoes the command using the command handler.
    /// </summary>
    /// <param name="command">The command to undo.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UndoAsync(TCommand command);
}