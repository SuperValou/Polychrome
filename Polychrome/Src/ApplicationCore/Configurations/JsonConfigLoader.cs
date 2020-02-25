using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using Kernel;

namespace ApplicationCore.Configurations
{
    public class JsonConfigLoader : IConfigLoader
    {
        private readonly ILogger _logger;
        private readonly Type _configurationType;
        private readonly IConfiguration _defaultConfiguration;
        
        public JsonConfigLoader(ILogger logger, Type configurationType, IConfiguration defaultConfiguration)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configurationType = configurationType ?? throw new ArgumentNullException(nameof(configurationType));
            _defaultConfiguration = defaultConfiguration ?? throw new ArgumentNullException(nameof(defaultConfiguration));
        }

        public IConfiguration LoadConfig(string configFilePath)
        {
            if (configFilePath == null)
            {
                throw new ArgumentNullException(nameof(configFilePath));
            }

            IConfiguration loadedConfig;

            if (configFilePath == string.Empty)
            {
                // use default location
                string currentExePath = Assembly.GetExecutingAssembly().Location;
                string defaultConfigPath = Path.ChangeExtension(currentExePath, ".config");

                if (File.Exists(defaultConfigPath))
                {
                    loadedConfig = ReadConfigFromJsonFile(defaultConfigPath);
                    
                }
                else
                {
                    loadedConfig = _defaultConfiguration;
                }
            }
            else
            {
                if (!File.Exists(configFilePath))
                {
                    _logger.Error($"Config file was not found at \"{configFilePath}\". " +
                                  $"Default configuration will be used instead.");
                    loadedConfig = _defaultConfiguration;
                }
                else
                {
                    loadedConfig = ReadConfigFromJsonFile(configFilePath);
                }
            }

            return loadedConfig;
        }

        private IConfiguration ReadConfigFromJsonFile(string configFilePath)
        {
            _logger.Debug("Reading config file at: " + configFilePath);
            
            try
            {
                string configurationText = File.ReadAllText(configFilePath);
                object configurationObject = JsonSerializer.Deserialize(configurationText, _configurationType);
                IConfiguration configuration = (IConfiguration) configurationObject;
                return configuration;
            }
            catch (Exception e)
            {
                _logger.Error($"Unable to read and load configuration from file, using default config instead. Details: {e.Message}", e);
                return _defaultConfiguration;
            }
        }
    }
}