using System.Threading.Tasks;
using NUnit.Framework;

public class CommandPatternTest
{
    private class State
    {
        public string Value { get; set; } = "";
    }

    private class TestCommand : UndoableCommand<TestCommand>
    {
        public override string CommandName => "BoardGame::Tests::Test";

        public string Message { get; }

        public State State { get; }

        public TestCommand(string message, State state)
        {
            Message = message;
            State = state;
        }
    }

    private class TestCommandHandler : IUndoableCommandHandler<TestCommand>
    {
        public Task ExecuteAsync(TestCommand command)
        {
            command.State.Value = command.Message;
            return Task.CompletedTask;
        }

        public Task UndoAsync(TestCommand command)
        {
            command.State.Value = "";
            return Task.CompletedTask;
        }

        public Task<bool> ValidateAsync(TestCommand command)
        {
            return Task.FromResult(true);
        }
    }

    private IUndoableCommandManager _commandManager;

    [SetUp]
    public void SetUp()
    {
        var commandDispatcher = new UndoableCommandDispatcher();
        commandDispatcher.Register(new TestCommandHandler());
        _commandManager = new UndoableCommandManager(
            commandDispatcher,
            new InMemoryCommandRecorder<IUndoableCommand>(),
            new InMemoryCommandRecorder<IUndoableCommand>(),
            new NullLogger()
        );
    }

    [Test]
    public async Task TestCommandExecution()
    {
        var state = new State();

        var cmd = new TestCommand("Test message", state);

        await _commandManager.ExecuteAsync(cmd);
        Assert.AreEqual("Test message", state.Value);

        try
        {
            await _commandManager.UndoAsync();
        }
        catch
        {
            Assert.Fail("Undo failed");
        }

        Assert.AreEqual("", state.Value);

        try
        {
            await _commandManager.UndoAsync();
            Assert.Fail("Expected exception not thrown");
        }
        catch
        {

        }

        try
        {
            await _commandManager.RedoAsync();
        }
        catch
        {
            Assert.Fail("Redo failed");
        }

        Assert.AreEqual("Test message", state.Value);

        try
        {
            await _commandManager.RedoAsync();
            Assert.Fail("Expected exception not thrown");
        }
        catch
        {

        }

        var cmd2 = new TestCommand("Test message 2", state);

        await _commandManager.ExecuteAsync(cmd2);

        Assert.AreEqual("Test message 2", state.Value);
    }
}