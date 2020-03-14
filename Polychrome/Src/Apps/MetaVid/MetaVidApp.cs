using ApplicationCore;
using ApplicationCore.Configurations;
using CliApplication;
using Kernel;
using MetaVid.Configurations;
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

            if (!Directory.Exists(_config.SourceFolder))
            {
                throw new ArgumentException($"{nameof(_config.SourceFolder)} doesn't exist at '{_config.SourceFolder}'.");                
            }

            if (_config.SourceExtensions == null || _config.SourceExtensions.Count == 0 || _config.SourceExtensions.Any(ext => ext == null))
            {
                throw new ArgumentException($"{nameof(_config.SourceExtensions)} is null, empty, or contains a null item.", nameof(config));
            }
        }

        protected override async Task<ICollection<IService>> InitializeServices()
        {
            await Task.Yield();
            return new List<IService>();
        }

        protected override async Task<int> RunMain()
        {
            ICollection<string> allowedExtension = new HashSet<string>(_config.SourceExtensions);
            foreach (var filePath in Directory.EnumerateFiles(_config.SourceFolder, "*.*", SearchOption.AllDirectories))
            {
                string fileExt = Path.GetExtension(filePath);
                if (!allowedExtension.Contains(fileExt))
                {
                    continue;
                }


            }

            await Task.Yield();            
            throw new NotImplementedException();
        }        
    }
}
