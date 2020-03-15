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

namespace MetaVid
{
    public class MetaVidApp : CliApp
    {
        private const string Name = "MetaVid";
        private const string Version = "0.1.0";

        private MetaVidConfig _config;

        public MetaVidApp() : base(Name, Version)
        {
        }

        protected override Type GetConfigType()
        {
            return typeof(MetaVidConfig);
        }

        protected override void ValidateConfig(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _config = config as MetaVidConfig;
            if (_config == null)
            {
                throw new ArgumentException($"{nameof(config)} was of invalid type {config.GetType().Name} instead of the expected type {nameof(MetaVidConfig)}.", nameof(config));
            }
        }

        protected override async Task<ICollection<IService>> InitializeServices()
        {
            await Task.Yield();
            return new List<IService>();
        }

        protected override async Task<int> RunMain()
        {
            if (_config.TaskList == null)
            {
                Logger.Warn("Noting to do.");
                return ExitCode.Success;
            }

            if (_config.TaskList.ProbeTaskSetup != null)
            {
                var probeTask = new ProbeTask(_config.TaskList.ProbeTaskSetup);
                //TaskManager.Schedule(probeTask);
            }

            
            
            throw new NotImplementedException();
        }        
    }
}
