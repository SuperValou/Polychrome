using System;

namespace SimpleLogs.LogsManagement
{
    internal class Logger : ILogger
    {
        private readonly string _name;
        private readonly ILogCentral _logCentral;

        public Logger(string name, ILogCentral logCentral)
        {
            _name = name;
            _logCentral = logCentral;
        }

        public void Trace(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Trace, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Debug(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Debug, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Info(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Info, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Warn(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Warning, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Warn(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Warning, message, exception);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Error(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Error, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Error(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Error, message, exception);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Fatal(string message)
        {
            var logEvent = new LogEvent(_name, LogLevel.Fatal, message);
            _logCentral.AddLogEvent(logEvent);
        }

        public void Fatal(string message, Exception exception)
        {
            var logEvent = new LogEvent(_name, LogLevel.Fatal, message, exception);
            _logCentral.AddLogEvent(logEvent);
        }


        public ILogger CreateSubLogger(string subLoggerName)
        {
            return new Logger($"{_name}/{subLoggerName}", _logCentral);
        }
    }
}