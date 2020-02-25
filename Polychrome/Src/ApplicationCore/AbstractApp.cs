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

        private IConfigLoader _configLoader;

        private bool _alreadyInitialized = false;
        
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

            if (_alreadyInitialized)
            {
                throw new AlreadyInitializedException(this.GetType().Name);
            }

            _alreadyInitialized = true;

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
            _configLoader = GetConfigLoader();
            IConfiguration config = _configLoader.LoadConfig(_argsParser.ParsedArgs.ConfigPath) ?? throw new NullReferenceException($"Configuration cannot be null. Did you forget to use {nameof(EmptyConfiguration)} instead?");
            if (config.AppName != AppName)
            {
                Logger.Error($"{config.GetType().Name} is a configuration for unknown app '{config.AppName}' instead of {AppName}.");
                return;
            }

            if (config.AppVersion != AppVersion)
            {
                Logger.Error($"{config.GetType().Name} is a configuration for {AppName} in unmanaged version '{config.AppVersion}'. " +
                             $"Current version of {AppName} is {AppVersion}. Did you forget to update the configuration?");
                return;
            }

            SetConfig(config);

            // other systems


            // ready
            IsInitialized = true;
            Logger.Info($"{AppName} {AppVersion} initialized.");
        }

        protected abstract void SetConfig(IConfiguration config);

        protected abstract IConfigLoader GetConfigLoader();

        public virtual void Boot()
        {
            if (Logger == null)
            {
                throw new NotInitializedException(this.GetType().Name);
            }

            if (!IsInitialized)
            {
                Logger.Error($"{AppName} {AppVersion} is not properly initialized.");
                return;
            }

            Run();
        }

        protected abstract void Run();

        public virtual void Dispose()
        {
            _logSystem?.Dispose();
        }
    }
}
