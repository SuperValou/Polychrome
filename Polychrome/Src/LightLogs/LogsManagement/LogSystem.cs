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
        private readonly ICollection<ITarget> _targets = new List<ITarget>();
        
        private ILogFlusher _logFlusher;
        private ILogger _rootLogger;

        public void AddDefaultConsoleTarget()
        {
            var consoleTarget = new ConsoleTarget();
            var consoleConfig = DefaultConfigFactory.GetDefaultConsoleTargetConfig();
            consoleTarget.Initialize(consoleConfig);
            AddTarget(consoleTarget);
        }

        public void AddDefaultFileTarget()
        {
            var fileTarget = new FileTarget();
            var fileTargetConfig = DefaultConfigFactory.GetDefaultFileTargetConfig();
            fileTarget.Initialize(fileTargetConfig);
            AddTarget(fileTarget);
        }

        public void AddTarget(ITarget target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (_rootLogger != null)
            {
                throw new AlreadyInitializedException(nameof(LogSystem), "Targets already set up.");
            }

            _targets.Add(target);
        }

        public ILogger Initialize()
        {
            return Initialize(LogLevel.Info);
        }

        public ILogger Initialize(LogLevel minLogLevel)
        {
            string rootLoggerName = AppDomain.CurrentDomain.FriendlyName;
            return Initialize(rootLoggerName, minLogLevel);
        }

        public ILogger Initialize(string rootLoggerName, LogLevel minLogLevel)
        {
            if (rootLoggerName == null)
            {
                throw new ArgumentNullException(nameof(rootLoggerName));
            }

            if (!Enum.IsDefined(typeof(LogLevel), minLogLevel))
            {
                throw new InvalidEnumArgumentException(nameof(minLogLevel), (int) minLogLevel, typeof(LogLevel));
            }
            
            if (_rootLogger != null)
            {
                throw new AlreadyInitializedException(nameof(LogSystem));
            }
            
            _logFlusher = new LogFlusher(_targets, minLogLevel);
            _logFlusher.Initialize();
            
            _rootLogger = new Logger(rootLoggerName, _logFlusher);
            _rootLogger.Debug($"{nameof(LogSystem)} armed, logging to {_targets.Count} targets.");

            return _rootLogger;
        }

        public void Dispose()
        {
            _logFlusher?.Dispose();
        }
    }
}