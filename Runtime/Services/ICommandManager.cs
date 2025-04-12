using System.Threading.Tasks;

/// <summary>
/// Interface for managing command history.
/// </summary>
public interface ICommandManager
{
    int HistoryCount { get; }

    /// <summary>
    /// Asynchronously executes a command and adds it to the history.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task ExecuteAsync(ICommand command);

    /// <summary>
    /// Queues a command for execution.
    /// This method is typically used for commands that are not executed immediately.
    /// </summary>
    /// <param name="command">The command to queue.</param>
    void QueueCommand(ICommand command);


    /// <summary>
    /// Serializes the command history using the provided serializer.
    /// </summary>
    /// <param name="serializer">The serializer to use for serialization.</param>
    /// <returns>A string representing the serialized command history.</returns>
    string SerializeHistory(ICommandSerializer serializer);
}