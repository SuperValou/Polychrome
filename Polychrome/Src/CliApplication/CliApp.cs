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
    }
}