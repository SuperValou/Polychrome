using System;

namespace ApplicationCore.Configurations
{
    public class EmptyConfiguration : IConfiguration
    {
        public string AppName { get; }
        public string AppVersion { get; }

        public EmptyConfiguration(string app, string version)
        {
            AppName = app ?? throw new ArgumentNullException(nameof(app));
            AppVersion = version ?? throw new ArgumentNullException(nameof(version));
        }
    }
}