using System;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using HelloWorldApp.Configurations.DTO;
using TmdbService.Configurations;

namespace HelloWorldApp
{
    public class TmdbCrawlerApp : CliApp
    {
        private const string Name = "HelloWorldApp";
        private const string Version = "0.1.0";

        private TmdbCrawlerConfiguration _config;

        public TmdbCrawlerApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(TmdbCrawlerConfiguration);
        }

        protected override async Task<int> Run(IConfiguration config)
        {
            if (config == null)
            {
                Logger.Fatal($"{nameof(config)} cannot be null.");
                return ExitCode.Error;
            }

            _config = config as TmdbCrawlerConfiguration;
            if (_config == null)
            {
                Logger.Fatal($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(TmdbCrawlerConfiguration)}.");
                return ExitCode.Error;
            }

            Logger.Info("Hello world!");
                                    
            return ExitCode.Success;
        }

    }
}