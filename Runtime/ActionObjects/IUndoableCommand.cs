using System.Threading.Tasks;

/// <summary>
/// Interface for commands that can be undone.
/// </summary>
public interface IUndoableCommand : ICommand
{
    /// <summary>
    /// Asynchronously undoes the command using the command dispatcher.
    /// </summary>
    /// <param name="dispatcher">The command dispatcher to undo the command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task UndoWithAsync(IUndoableCommandDispatcher dispatcher);
}