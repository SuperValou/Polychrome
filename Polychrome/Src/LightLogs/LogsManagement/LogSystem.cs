using System;
using System.Collections.Generic;
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
        
        private LogFlusher _logFlusher;
        private ILogger _rootLogger;
        
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
            string rootLoggerName = AppDomain.CurrentDomain.FriendlyName;
            return Initialize(rootLoggerName);
        }

        public ILogger Initialize(string rootLoggerName)
        {
            if (rootLoggerName == null)
            {
                throw new ArgumentNullException(nameof(rootLoggerName));
            }
            
            if (_rootLogger != null)
            {
                throw new AlreadyInitializedException(nameof(LogSystem));
            }
            
            _logFlusher = new LogFlusher(_targets);
            
            _rootLogger = new Logger(rootLoggerName, _logFlusher);

            return _rootLogger;
        }

        public void Dispose()
        {
            _logFlusher?.Dispose();
        }
    }
}