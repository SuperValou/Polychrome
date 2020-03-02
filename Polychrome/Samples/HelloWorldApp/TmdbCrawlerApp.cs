using System;
using ApplicationCore.Configurations;
using CliApplication;
using HelloWorldApp.Configurations.DTO;

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

        protected override void Run(IConfiguration config)
        {
            if (config == null)
            {
                Logger.Fatal($"{nameof(config)} cannot be null.");
                return;
            }

            Logger.Info("Hello world!");

            _config = config as TmdbCrawlerConfiguration;
            if (_config == null)
            {
                Logger.Fatal
            }
        }
    }
}