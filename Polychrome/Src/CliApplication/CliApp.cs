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

        protected override IConfigManager GetConfigManager()
        {
            var configurationType = GetConfigurationType();
            var defaultConfig = new EmptyConfiguration(AppName, AppVersion);
            var logger = Logger.CreateSubLogger(nameof(JsonConfigManager));
            return new JsonConfigManager(logger, configurationType, defaultConfig);
        }

        protected abstract Type GetConfigurationType();
    }
}