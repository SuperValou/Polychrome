using System;
using System.Collections.Generic;
using Kernel;
using LightLogs.Targets;
using LightLogs.Configs;
using LightLogs.Targets;

namespace LightLogs.LogsManagement
{
    public class LoggerSystem : IDisposable
    {
        private static LoggerSystem _systemInstance;

        private readonly ICollection<ITarget> _targets = new List<ITarget>();
        
        private LogCentral _logCentral;
        private ILogger _rootLogger;

        public bool FileTargetEnabled { get; set; } = true;
        public FileTargetConfig FileTargetConfig { get; } = new FileTargetConfig();

        public bool ConsoleTargetEnabled { get; set; } = true;
        public ConsoleTargetConfig ConsoleTargetConfig { get; } = new ConsoleTargetConfig();

        private LoggerSystem()
        {
        }

        public static LoggerSystem GetInstance()
        {
            if (_systemInstance != null)
            {
                return _systemInstance;
            }

            _systemInstance = new LoggerSystem();
            return _systemInstance;
        }

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

        public ILogger GetOrCreateLogger()
        {
            if (_rootLogger != null)
            {
                return _rootLogger;
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
            _targets.Clear();

            _rootLogger = new Logger(AppDomain.CurrentDomain.FriendlyName, _logCentral);

            return _rootLogger;
        }

        public void Dispose()
        {
            _logCentral?.Dispose();
        }
    }
}