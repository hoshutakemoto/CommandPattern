using System;

public sealed class NullLogger : ILogger
{
    public void Verbose(string message) { }
    public void Debug(string message) { }
    public void Info(string message) { }
    public void Warning(string message) { }
    public void Error(string message, Exception ex = null) { }
    public void Fatal(string message, Exception ex = null) { }
}