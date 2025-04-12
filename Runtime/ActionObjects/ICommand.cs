using System.Threading.Tasks;

/// <summary>
/// Interface representing a command in the command pattern.
/// </summary>
public interface ICommand
{
    /// <summary>
    /// Gets the name of the command.
    /// </summary>
    string CommandName { get; }

    /// <summary>
    /// Asynchronously executes the command using the command dispatcher.
    /// </summary>
    /// <param name="dispatcher">The command dispatcher to execute the command.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteWithAsync(ICommandDispatcher dispatcher);

    /// <summary>
    /// Asynchronously validates the command using the command dispatcher.
    /// </summary>
    /// <param name="dispatcher">The command dispatcher to validate the command.</param>
    /// <returns>A task representing the asynchronous operation, with a boolean result indicating validity.</returns>
    Task<bool> ValidateWithAsync(ICommandDispatcher dispatcher);

    /// <summary>
    /// Serializes the command to a string representation.
    /// </summary>
    /// <param name="serializer">The command serializer to use for serialization.</param>
    /// <returns>The string representation of the command.</returns>
    string SerializeWith(ICommandSerializer serializer);
}