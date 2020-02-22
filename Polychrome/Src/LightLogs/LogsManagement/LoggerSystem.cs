using System;
using System.Collections.Generic;
using Kernel;
using Kernel.Exceptions;
using LightLogs.Targets;
using LightLogs.Configs;

namespace LightLogs.LogsManagement
{
    public class LoggerSystem : ILoggerSystem
    {
        private readonly ICollection<ITarget> _targets = new List<ITarget>();
        
        private LogCentral _logCentral;
        private ILogger _rootLogger;

        public bool FileTargetEnabled { get; set; } = true;
        public FileTargetConfig FileTargetConfig { get; } = new FileTargetConfig();

        public bool ConsoleTargetEnabled { get; set; } = true;
        public ConsoleTargetConfig ConsoleTargetConfig { get; } = new ConsoleTargetConfig();
        
        public void AddTarget(ITarget target)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (_rootLogger != null)
            {
                throw new InvalidOperationException($"{nameof(LoggerSystem)} already have set up its target.");
            }

            _targets.Add(target);
        }

        public ILogger InitializeLogger()
        {
            string rootLoggerName = AppDomain.CurrentDomain.FriendlyName;
            return InitializeLogger(rootLoggerName);
        }

        public ILogger InitializeLogger(string rootLoggerName)
        {
            if (rootLoggerName == null)
            {
                throw new ArgumentNullException(nameof(rootLoggerName));
            }
            
            if (_rootLogger != null)
            {
                throw new AlreadyInitializedException(nameof(LoggerSystem));
            }

            if (this.ConsoleTargetEnabled)
            {
                var consoleTarget = new ConsoleTarget();
                consoleTarget.Initialize(ConsoleTargetConfig);
                _targets.Add(consoleTarget);
            }

            if (this.FileTargetEnabled)
            {
                var fileTarget = new FileTarget();
                fileTarget.Initialize(FileTargetConfig);
                _targets.Add(fileTarget);
            }
            
            _logCentral = new LogCentral(_targets);
            
            _rootLogger = new Logger(rootLoggerName, _logCentral);

            return _rootLogger;
        }

        public void Dispose()
        {
            _logCentral?.Dispose();
        }
    }
}