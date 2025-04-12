using System.Threading.Tasks;

/// <summary>
/// Interface for dispatching commands.
/// This interface provides methods to execute and validate commands.
/// </summary>
public interface ICommandDispatcher
{
    /// <summary>
    /// Asynchronously executes a command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync<TCommand>(TCommand command) where TCommand : ICommand;

    /// <summary>
    /// Asynchronously validates a command.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    /// <param name="command">The command to validate.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating if the command is valid.</returns>
    Task<bool> ValidateAsync<TCommand>(TCommand command) where TCommand : ICommand;
}