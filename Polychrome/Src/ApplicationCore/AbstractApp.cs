using System;
using System.Collections.Generic;

namespace ApplicationCore
{
    public abstract class AbstractApp
    {
        public string AppName { get; }
        public string Version { get; }

        protected AbstractApp(string appName, string version)
        {
            if (string.IsNullOrWhiteSpace(appName))
            {
                throw new ArgumentException($"{nameof(appName)} cannot be null or empty.", nameof(appName));
            }

            AppName = appName;
            Version = version ?? throw new ArgumentNullException(nameof(version));
        }


        public void Initialize(ICollection<string> args)
        {
            throw new NotImplementedException();
        }

        public void Boot()
        {
            throw new NotImplementedException();
        }
    }
}
