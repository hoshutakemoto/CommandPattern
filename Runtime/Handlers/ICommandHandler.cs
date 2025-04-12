using System.Threading.Tasks;

/// <summary>
/// Interface for command handler.
/// The command handler is responsible for executing the command and performing any necessary actions.
/// </summary>
/// <typeparam name="TCommand">The type of the command.</typeparam>
public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    /// <summary>
    /// Asynchronously executes the command using the command handler.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(TCommand command);

    /// <summary>
    /// Asynchronously validates the command using the command handler.
    /// </summary>
    /// <param name="command">The command to validate.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating whether the command is valid.</returns>
    Task<bool> ValidateAsync(TCommand command);
}