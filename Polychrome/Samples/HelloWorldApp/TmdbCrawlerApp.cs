using System;
using CliApplication;
using HelloWorldApp.Configurations.DTO;

namespace HelloWorldApp
{
    public class TmdbCrawlerApp : CliApp
    {
        private const string Name = "HelloWorldApp";
        private const string Version = "0.1.0";

        public TmdbCrawlerApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigurationType()
        {
            return typeof(TmdbCrawlerConfiguration);
        }
    }
}