using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using HelloWorldApp.Configurations.DTO;
using Kernel;
using Tmdb.Service;

namespace HelloWorldApp
{
    public class TmdbCrawlerApp : CliApp
    {
        private const string Name = "TmdbCrawler";
        private const string Version = "0.1.0";

        private TmdbCrawlerConfiguration _config;

        public TmdbCrawlerApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(TmdbCrawlerConfiguration);
        }
        
        protected override void ValidateConfig(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _config = config as TmdbCrawlerConfiguration;
            if (_config == null)
            {
                throw new ArgumentException($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(TmdbCrawlerConfiguration)}.", nameof(config));
            }
        }

        protected override async Task<ICollection<IService>> InitializeServices()
        {
            var services = new List<IService>();

            // tmdb
            var tmdbLogger = Logger.CreateSubLogger(nameof(TmdbServiceClient));
            ITmdbService tmdbService = new TmdbServiceClient(tmdbLogger, null);
            await Task.Yield();

            return services;
        }

        protected override async Task<int> RunMain()
        {
            Logger.Info("Hello world!");
            await Task.Yield();

            return ExitCode.Success;
        }
    }
}