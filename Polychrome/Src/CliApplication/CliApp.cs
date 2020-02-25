using System;
using ApplicationCore;
using ApplicationCore.Configurations;

namespace CliApplication
{
    public abstract class CliApp : AbstractApp
    {
        protected CliApp(string appName, string appVersion) 
            : base(appName, appVersion)
        {
        }

        protected override IConfigLoader GetConfigLoader()
        {
            var configurationType = GetConfigType();
            var defaultConfig = new EmptyConfiguration(AppName, AppVersion);
            var logger = Logger.CreateSubLogger(nameof(JsonConfigLoader));
            return new JsonConfigLoader(logger, configurationType, defaultConfig);
        }

        protected abstract Type GetConfigType();
    }
}