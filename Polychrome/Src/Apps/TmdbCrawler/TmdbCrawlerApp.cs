using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using Kernel;
using Tmdb.Service;
using TmdbCrawler.Configurations;
using TmdbCrawler.Tasks;

namespace TmdbCrawler
{
    public class TmdbCrawlerApp : CliApp
    {
        private const string Name = "TmdbCrawler";
        private const string Version = "0.1.0";

        private TmdbCrawlerConfiguration _config;

        private ITmdbService _tmdbService;

        public TmdbCrawlerApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(TmdbCrawlerConfiguration);
        }
        
        protected override bool ValidateConfig(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            if (config is EmptyConfiguration)
            {
                Logger.Error($"Configuration cannot be empty.");
                return false;
            }
            
            _config = config as TmdbCrawlerConfiguration;
            if (_config == null)
            {
                Logger.Error($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(TmdbCrawlerConfiguration)}");
                return false;
            }

            return true;
        }

        protected override async IAsyncEnumerable<IService> InitializeServices()
        {
            // tmdb
            var tmdbLogger = Logger.CreateSubLogger(nameof(TmdbService));
            _tmdbService = new TmdbService(tmdbLogger);
            await _tmdbService.Initialize(_config.Services.TmdbServiceConfig);

            yield return _tmdbService;
        }

        protected override async Task<int> RunMain()
        {
            DumpExportsTask dumpExportsTask = new DumpExportsTask(_config.TasksToRun.DumpExports, _tmdbService);
            
            Logger.Info("Hello world!");
            await Task.Yield();

            return ExitCode.Success;
        }
    }
}