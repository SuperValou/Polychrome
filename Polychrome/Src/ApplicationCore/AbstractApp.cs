using System;
using System.Collections.Generic;
using ApplicationCore.ArgParsing;
using Kernel;
using Kernel.Exceptions;
using LightLogs;
using LightLogs.API;
using LightLogs.LogsManagement;

namespace ApplicationCore
{
    public abstract class AbstractApp
    {
        private readonly ArgsParser _argsParser = new ArgsParser();
        private readonly ILogSystem _logSystem = new LogSystem();

        private bool _systemsReady = false;

        protected ILogger Logger { get; private set; }

        public string AppName { get; }
        public string Version { get; }

        public bool IsInitialized { get; private set; }
        public bool IsRunning { get; private set; }

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

            if (IsInitialized)
            {
                throw new AlreadyInitializedException(this.GetType().Name);
            }

            // parse args
            _argsParser.Parse(args);

            // arm log system
            if (_argsParser.HasError)
            {
                Logger = _logSystem.Initialize(LogLevel.Fatal);
                Logger.Fatal($"Invalid arguments: '{string.Join(" ", args)}'. {_argsParser.ErrorMessage}");
                return;
            }
            
            if (_argsParser.ParsedArgs.DisableLogs)
            {
                Logger = new EmptyLogger();
            }
            else
            {
                Logger = _logSystem.Initialize(_argsParser.ParsedArgs.MinLogLevel);
            }

            // arm systems
            // arm config file

            _systemsReady = true;
            throw new NotImplementedException();
        }

        public void Boot()
        {
            if (Logger == null)
            {
                throw new NotInitializedException(this.GetType().Name);
            }

            if (!_systemsReady)
            {
                Logger.Error("Systems are not properly set up.");
                return;
            }

            throw new NotImplementedException();
        }

        private void ParseArgs(ICollection<string> args)
        {
            throw new NotImplementedException();
        }        
    }
}
