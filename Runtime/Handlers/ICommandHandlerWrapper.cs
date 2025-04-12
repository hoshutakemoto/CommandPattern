using System.Threading.Tasks;

/// <summary>
/// Interface for a command handler wrapper.
/// </summary>
public interface ICommandHandlerWrapper
{
    /// <summary>
    /// Asynchronously executes the command using the command handler.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(ICommand command);

    /// <summary>
    /// Asynchronously validates the command using the command handler.
    /// </summary>
    /// <param name="command">The command to validate.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating whether the command is valid.</returns>
    Task<bool> ValidateAsync(ICommand command);
}