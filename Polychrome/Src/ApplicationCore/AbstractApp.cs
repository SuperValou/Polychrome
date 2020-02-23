using System;
using System.Collections.Generic;
using ApplicationCore.ArgParsing;
using Kernel;
using LightLogs.API;
using LightLogs.LogsManagement;

namespace ApplicationCore
{
    public abstract class AbstractApp
    {
        private readonly ILogSystem _logSystem = new LogSystem();

        protected ILogger Logger { get; }

        public string AppName { get; }
        public string Version { get; }
              

        protected AbstractApp(string appName, string version)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentException($"{nameof(appName)} cannot be null or empty.", nameof(appName));
            }

            if (string.IsNullOrEmpty(version))
            {
                throw new ArgumentException($"{nameof(version)} cannot be null or empty.", nameof(version));
            }

            AppName = appName;
            Version = version;
        }


        public void Initialize(ICollection<string> args)
        {
            if (args == null)
            {
                throw new ArgumentNullException(nameof(args));
            }

            ParseArgs(args);

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

        private void ParseArgs(ICollection<string> args)
        {
            throw new NotImplementedException();
        }        
    }
}
