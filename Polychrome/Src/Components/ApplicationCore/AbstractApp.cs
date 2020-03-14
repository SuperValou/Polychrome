using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly ILogSystem _logSystem = new LogSystem();
        private readonly ICollection<IService> _services = new List<IService>();

        private IArgsParser _argsParser;
        private IConfigLoader _configLoader;
        
        private IConfiguration _config;
        
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


        public virtual async Task Initialize(ICollection<string> args)
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
            _argsParser = GetArgsParser() ?? throw new InvalidOperationException($"The {nameof(IArgsParser)} object returned by the {nameof(GetArgsParser)} method cannot be null.");
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
            _configLoader = GetConfigLoader() ?? throw new InvalidOperationException($"The {nameof(IConfigLoader)} object returned by the {nameof(GetConfigLoader)} method cannot be null.");
            _config = await _configLoader.LoadConfig(_argsParser.ParsedArgs.ConfigPath) ?? throw new InvalidOperationException($"The configuration object cannot be null. Did you forget to use {nameof(EmptyConfiguration)} instead?");
            if (_config.AppName != AppName)
            {
                Logger.Error($"{_config.GetType().Name} is a configuration for unknown app '{_config.AppName}' instead of {AppName}.");
                return;
            }

            if (_config.AppVersion != AppVersion)
            {
                Logger.Error($"{_config.GetType().Name} is a configuration for {AppName} in unmanaged version '{_config.AppVersion}'. " +
                             $"Current version of {AppName} is {AppVersion}. Did you forget to update the configuration?");
                return;
            }

            // initialize services
            Logger.Debug($"Initializing services...");
            ICollection<IService> services;
            try
            {
                services = await InitializeServices(_config) ?? throw new InvalidOperationException($"The collection of {nameof(IService)} returned by the {nameof(InitializeServices)} method cannot be null. Did you intended to return an empty list instead?");
            }
            catch (Exception e)
            {
                Logger.Error($"Error while initializing services: {e.Message}", e);
                return;
            }

            foreach (var service in services)
            {
                if (service == null)
                {
                    throw new InvalidOperationException($"One of the service returned by the {nameof(InitializeServices)} method was null.");
                }

                if (!service.IsInitialized)
                {
                    Logger.Warn($"{service} is not initialized.");
                }

                _services.Add(service);
            }

            // ready
            IsInitialized = true;
            Logger.Debug($"{AppName} {AppVersion} initialized.");
        }
               

        public virtual async Task<int> Run()
        {
            if (Logger == null)
            {
                throw new NotInitializedException(this.GetType().Name);
            }

            if (!IsInitialized)
            {
                Logger.Error($"{AppName} {AppVersion} is not properly initialized.");
                return ExitCode.Error;
            }

            int exitCode = await Run(_config);
            return exitCode;
        }

        protected virtual IArgsParser GetArgsParser()
        {
            var argsParser = new ArgsParser();
            return argsParser;
        }

        protected virtual IConfigLoader GetConfigLoader()
        {
            Type configurationType = GetConfigType() ?? throw new InvalidOperationException($"Type returned by the {nameof(GetConfigType)} cannot be null.");
            if (!typeof(IConfiguration).IsAssignableFrom(configurationType))
            {
                throw new InvalidOperationException($"Configuration type '{configurationType.FullName}' must implement the {nameof(IConfiguration)} interface.");
            }

            IConfiguration defaultConfig = new EmptyConfiguration(AppName, AppVersion);
            ILogger logger = Logger.CreateSubLogger(nameof(JsonConfigLoader));
            return new JsonConfigLoader(logger, configurationType, defaultConfig);
        }

        protected abstract Type GetConfigType();

        protected abstract Task<ICollection<IService>> InitializeServices(IConfiguration config);

        protected abstract Task<int> Run(IConfiguration config);

        public virtual void Dispose()
        {
            foreach (var service in _services)
            {
                service.Dispose();
            }

            _logSystem.Dispose();            
        }
    }
}
