using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using Kernel;
using MediaDatabase.Service;
using SwapFusion.Configurations;
using SwapFusion.Tasks;

namespace SwapFusion
{
    public class SwapFusionApp : CliApp
    {
        private const string Name = "SwapFusion";
        private const string Version = "0.1.0";

        private SwapFusionConfig _config;

        private IMediaDatabaseService _mediaDatabaseService;

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

        protected override async IAsyncEnumerable<IService> InitializeServices()
        {
            _mediaDatabaseService = new MediaDatabaseService();
            await _mediaDatabaseService.Initialize(_config.Services.MediaDatabaseServiceConfig);
            yield return _mediaDatabaseService;
        }

        protected override async Task<int> RunMain()
        {
            ILogger swpasLogger = Logger.CreateSubLogger(nameof(GenerateSwapsTask));
            var swapsTask = new GenerateSwapsTask(swpasLogger, _config.TaskList.GenerateSwapsTaskSetup, _mediaDatabaseService);
            await swapsTask.Initialize(_config.TaskList.WorkingDirectory);
            await swapsTask.Execute();
            return ExitCode.Success;
        }
    }
}