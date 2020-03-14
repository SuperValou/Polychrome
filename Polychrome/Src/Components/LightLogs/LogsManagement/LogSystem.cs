using System;
using System.Collections.Generic;
using System.ComponentModel;
using Kernel;
using Kernel.Exceptions;
using LightLogs.API;
using LightLogs.Targets;
using LightLogs.Configs;

namespace LightLogs.LogsManagement
{
    public class LogSystem : ILogSystem
    {
        private ILogFlusher _logFlusher;
        private ILogger _rootLogger;

        public string DefaultLogFilePath { get; private set; } = string.Empty;

        public ILogger Initialize()
        {
            return Initialize(LogLevel.Trace);
        }

        public ILogger Initialize(LogLevel minLogLevel)
        {
            string rootLoggerName = AppDomain.CurrentDomain.FriendlyName;
            return Initialize(rootLoggerName, minLogLevel);
        }

        public ILogger Initialize(string rootLoggerName, LogLevel minLogLevel)
        {
            ICollection<ITarget> defaultTargets = new List<ITarget>();

            var consoleTarget = new ConsoleTarget();
            var consoleConfig = DefaultConfigFactory.GetDefaultConsoleTargetConfig();
            consoleTarget.Initialize(consoleConfig);
            defaultTargets.Add(consoleTarget);

            var fileTarget = new FileTarget();
            var fileTargetConfig = DefaultConfigFactory.GetDefaultFileTargetConfig();
            fileTarget.Initialize(fileTargetConfig);
            DefaultLogFilePath = fileTarget.LogFilePath;
            defaultTargets.Add(fileTarget);

            return Initialize(rootLoggerName, minLogLevel, defaultTargets);
        }

        public ILogger Initialize(string rootLoggerName, LogLevel minLogLevel, ICollection<ITarget> targets)
        {
            if (string.IsNullOrEmpty(rootLoggerName))
            {
                throw new ArgumentException($"{nameof(rootLoggerName)} cannot be null or empty.", nameof(rootLoggerName));
            }

            if (!Enum.IsDefined(typeof(LogLevel), minLogLevel))
            {
                throw new InvalidEnumArgumentException(nameof(minLogLevel), (int) minLogLevel, typeof(LogLevel));
            }

            if (targets == null)
            {
                throw new ArgumentNullException(nameof(targets));
            }
            
            if (_rootLogger != null)
            {
                throw new AlreadyInitializedException(nameof(LogSystem));
            }
            
            _logFlusher = new LogFlusher(targets, minLogLevel);
            _logFlusher.Initialize();
            
            _rootLogger = new Logger(rootLoggerName, _logFlusher);
            _rootLogger.Trace($"{nameof(LogSystem)} armed, logging to {targets.Count} targets.");

            return _rootLogger;
        }

        public void Dispose()
        {
            _logFlusher?.Dispose();
        }
    }
}