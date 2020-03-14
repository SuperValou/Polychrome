using System;

namespace LightLogs
{
    internal class LogEvent
    {
        public string Owner { get; }

        public LogLevel Level { get; }

        public string Message { get; }

        public DateTime Timestamp { get; }

        public Exception Exception { get; }

        public LogEvent(string owner, LogLevel level, string message) : this(owner, level, message, null)
        {
        }

        public LogEvent(string owner, LogLevel level, string message, Exception exception)
        {
            Timestamp = DateTime.Now;
            Owner = owner;
            Level = level;
            Message = message;
            Exception = exception;
        }
    }
}