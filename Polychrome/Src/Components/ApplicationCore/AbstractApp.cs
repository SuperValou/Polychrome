using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.ArgParsing;
using ApplicationCore.Configurations;
using Kernel;
using Kernel.Exceptions;
using LightLogs;
using LightLogs.API;
using LightLogs.LogsManagement;
using TaskSystem;

namespace ApplicationCore
{
    public abstract class AbstractApp : IApp
    {
        private readonly ILogSystem _logSystem = new LogSystem();
        private readonly ICollection<IService> _services = new List<IService>();

        private IArgsParser _argsParser;
        private IConfigLoader _configLoader;
        
        private bool _alreadyInitialized = false;
        
        protected ILogger Logger { get; private set; }
        protected ITaskManager TaskManager { get; private set; }
        
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


        public async Task Initialize(ICollection<string> args)
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
            Logger.Debug($"Loading config...");
            _configLoader = GetConfigLoader() ?? throw new InvalidOperationException($"The {nameof(IConfigLoader)} object returned by the {nameof(GetConfigLoader)} method cannot be null.");
            IConfiguration config = await _configLoader.LoadConfig(_argsParser.ParsedArgs.ConfigPath) ?? throw new InvalidOperationException($"The configuration object cannot be null. Did you forget to use {nameof(EmptyConfiguration)} instead?");
                        
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

            try
            {
                ValidateConfig(config);
            }
            catch (Exception e)
            {
                Logger.Error($"Configuration error: {e.Message}", e);
                return;
            }

            Logger.Debug($"Loaded {config.GetType().Name}.");

            // initialize task system
            Logger.Debug($"Setting up task system...");
            TaskManager = GetTaskManager() ?? throw new InvalidOperationException($"The {nameof(ITaskManager)} object returned by the {nameof(GetTaskManager)} method cannot be null.");
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string taskManagerDirectory = Path.Combine(appDataPath, "Polychrome", AppName, AppVersion);
            TaskManager.Initialize(taskManagerDirectory);
            Logger.Debug($"Task system ready.");

            // initialize services
            Logger.Debug($"Initializing services...");
            ICollection<IService> services;
            try
            {
                services = await InitializeServices() ?? throw new InvalidOperationException($"The collection of {nameof(IService)} returned by the {nameof(InitializeServices)} method cannot be null. Did you intended to return an empty list instead?");
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

            Logger.Debug($"Services ready.");

            // ready
            IsInitialized = true;
            Logger.Info($"{AppName} {AppVersion} initialized.");
        }
               

        public async Task<int> Run()
        {
            if (Logger == null)
            {
                throw new NotInitializedException(this.GetType().Name);
            }

            Logger.Debug($"Running {AppName} {AppVersion}...");

            if (!IsInitialized)
            {
                Logger.Error($"{AppName} {AppVersion} is not properly initialized.");
                return ExitCode.Error;
            }

            int exitCode = await RunMain();
            Logger.Info($"{AppName} {AppVersion} exited with code {exitCode}.");
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

        protected virtual ITaskManager GetTaskManager()
        {
            var logger = Logger.CreateSubLogger(nameof(TaskManager));
            return new TaskManager(logger);
        }

        protected abstract Type GetConfigType();

        protected abstract void ValidateConfig(IConfiguration config);

        protected abstract Task<ICollection<IService>> InitializeServices();

        protected abstract Task<int> RunMain();

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
