using System;

namespace ApplicationCore.Configurations
{
    public class EmptyConfiguration : IConfiguration
    {
        public string App { get; }
        public string Version { get; }

        public EmptyConfiguration(string app, string version)
        {
            App = app ?? throw new ArgumentNullException(nameof(app));
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }
    }
}