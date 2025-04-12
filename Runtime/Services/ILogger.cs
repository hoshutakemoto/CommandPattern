using System;

public interface ILogger
{
    void Verbose(string message);
    void Debug(string message);
    void Info(string message);
    void Warning(string message);
    void Error(string message, Exception ex = null);
    void Fatal(string message, Exception ex = null);
}