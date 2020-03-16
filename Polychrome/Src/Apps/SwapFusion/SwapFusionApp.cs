using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore.Configurations;
using CliApplication;
using Kernel;
using SwapFusion.Configurations;

namespace SwapFusion
{
    public class SwapFusionApp : CliApp
    {
        private const string Name = "SwapFusion";
        private const string Version = "0.1.0";

        private SwapFusionConfig _config;

        public SwapFusionApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(SwapFusionConfig);
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

            _config = config as SwapFusionConfig;
            if (_config == null)
            {
                Logger.Error($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(SwapFusionConfig)}");
                return false;
            }
            
            return true;
        }

        protected override IAsyncEnumerable<IService> InitializeServices()
        {
            throw new NotImplementedException();
        }

        protected override Task<int> RunMain()
        {
            throw new NotImplementedException();
        }
    }
}