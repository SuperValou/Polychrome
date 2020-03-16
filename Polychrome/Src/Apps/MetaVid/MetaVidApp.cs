﻿using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using Kernel;
using MetaVid.Configurations;
using MetaVid.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MediaDatabase.Service;

namespace MetaVid
{
    public class MetaVidApp : CliApp
    {
        private const string Name = "MetaVid";
        private const string Version = "0.1.0";

        private MetaVidConfig _config;

        private IMediaDatabaseService _mediaDatabaseService;

        public MetaVidApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(MetaVidConfig);
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

            _config = config as MetaVidConfig;
            if (_config == null)
            {
                Logger.Error($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(MetaVidConfig)}");
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
            // run tasks
            var probeTasklogger = Logger.CreateSubLogger(nameof(ProbeTask));
            var probeTask = new ProbeTask(probeTasklogger, _config.TaskList.ProbeTaskSetup, _mediaDatabaseService);
            await probeTask.Initialize(_config.TaskList.WorkingDirectory);
            await probeTask.Execute();

            return ExitCode.Success;
        }        
    }
}
