using System.Collections.Generic;

/// <summary>
/// Interface for serializing commands.
/// This interface defines a method for serializing commands into a string format.
/// It is used to convert command objects into a format that can be easily stored or transmitted.
/// </summary>
public interface ICommandSerializer
{
    /// <summary>
    /// Serializes a command into a string format.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to serialize.</typeparam>
    /// <param name="command">The command object to serialize.</param>
    /// <returns>A string representation of the serialized command.</returns>
    public string Serialize<TCommand>(TCommand command) where TCommand : ICommand;

    /// <summary>
    /// Serializes a collection of commands into a string format.
    /// </summary>
    /// <typeparam name="TCommand">The type of the command to serialize.</typeparam>
    /// <param name="command">The collection of command objects to serialize.</param>
    /// <returns>A string representation of the serialized commands.</returns>
    public string Serialize<TCommand>(IEnumerable<TCommand> command) where TCommand : ICommand;

    /// <summary>
    /// Deserializes a command from a string.
    /// </summary>
    /// <param name="serializedCommand">The serialized command string.</param>
    /// <returns>The deserialized command object.</returns>
    public ICommand Deserialize(string serializedCommand);
}