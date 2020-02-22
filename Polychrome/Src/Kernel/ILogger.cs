using System;

namespace SimpleLogs
{
    public interface ILogger
    {
        void Trace(string message);

        void Debug(string message);

        void Info(string message);

        void Warn(string message);
        void Warn(string message, Exception exception);

        void Error(string message, Exception exception);
        void Error(string message);

        void Fatal(string message);
        void Fatal(string message, Exception exception);

        ILogger CreateSubLogger(string subLoggerName);
    }
}