using System;
using System.Collections.Generic;
using ApplicationCore.ArgParsing;
using ApplicationCore.Configurations;
using Kernel;
using Kernel.Exceptions;
using LightLogs;
using LightLogs.API;
using LightLogs.LogsManagement;

namespace ApplicationCore
{
    public abstract class AbstractApp : IApp
    {
        private readonly ArgsParser _argsParser = new ArgsParser();
        private readonly ILogSystem _logSystem = new LogSystem();

        private IConfigManager _configManager;

        private bool _systemsReady = false;

        protected ILogger Logger { get; private set; }
        
        public string AppName { get; }
        public string AppVersion { get; }

        public bool IsInitialized { get; private set; }
        public bool IsRunning { get; private set; }

        protected AbstractApp(string appName, string appVersion)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentException($"{nameof(appName)} cannot be null or empty.", nameof(appName));
            }

            if (string.IsNullOrEmpty(appVersion))
            {
                throw new ArgumentException($"{nameof(appVersion)} cannot be null or empty.", nameof(appVersion));
            }

            AppName = appName;
            AppVersion = appVersion;
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
            
            // load config
            _configManager = GetConfigManager();
            _configManager.LoadConfig(_argsParser.ParsedArgs.ConfigPath);

            // ready
            _systemsReady = true;
        }

        protected abstract IConfigManager GetConfigManager();

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

            // vroom
            Logger.Info("Hello world!");
        }

        public void Dispose()
        {
            _logSystem?.Dispose();
        }
    }
}
