using Kernel;
using System;

namespace LightLogs.LogsManagement
{
    internal class Logger : ILogger
    {
        private readonly string _name;
        private readonly ILogFlusher _logFlusher;

        public Logger(string name, ILogFlusher logFlusher)
        {
            _name = name;
            _logFlusher = logFlusher;
        }

        public void Trace(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Trace, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Debug(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Debug, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Info(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Info, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Warn(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Warning, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Warn(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Warning, message, exception);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Error(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Error, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Error(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Error, message, exception);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Fatal(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Fatal, message);
            _logFlusher.AddLogEvent(logEvent);
        }

        public void Fatal(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Fatal, message, exception);
            _logFlusher.AddLogEvent(logEvent);
        }


        public ILogger CreateSubLogger(string subLoggerName)
        {
            return new Logger($"{_name}/{subLoggerName}", _logFlusher);
        }
    }
}