using System;
using System.Collections.Generic;
using Kernel;
using LightLogs.API;
using LightLogs.LogsManagement;

namespace ApplicationCore
{
    public abstract class AbstractApp
    {
        private readonly ILogSystem _logSystem = new LogSystem();

        public string AppName { get; }
        public string Version { get; }

        protected ILogger Logger { get; }

        protected AbstractApp(string appName, string version)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentException($"{nameof(appName)} cannot be null or empty.", nameof(appName));
            }

            AppName = appName;
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }


        public void Initialize(ICollection<string> args)
        {
            // parse args
            // arm logger (logs --disable or logs --nofile)
            // arm systems
            // arm config (config -l or config -f "c:/here.config"
            // arm other options (commit -m "stuff")
            throw new NotImplementedException();
        }

        public void Boot()
        {
            throw new NotImplementedException();
        }
    }
}
