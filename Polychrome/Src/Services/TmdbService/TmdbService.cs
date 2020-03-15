using Kernel;
using System;
using System.Threading.Tasks;
using Kernel.Exceptions;
using Tmdb.Service.Configurations;
using Tmdb.Service.Exports;

namespace Tmdb.Service
{
    public class TmdbService : ITmdbService
    {
        private readonly ILogger _logger;
        private IExportManager _exportManager;

        public bool IsInitialized { get; private set; }

        public TmdbService(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Initialize(TmdbServiceConfig config)
        {
            return Task.Run(() =>
            {
                if (config == null)
                {
                    throw new ArgumentNullException(nameof(config));
                }

                if (IsInitialized)
                {
                    throw new AlreadyInitializedException(nameof(TmdbService));
                }

                var subLogger = _logger.CreateSubLogger(nameof(ExportManager));
                _exportManager = new ExportManager(subLogger, config.ExportsRoot);

                IsInitialized = true;
            });
        }

        public Task<string> DownloadExport(string idType, DateTime date, string outputDirectory, bool force)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            
        }
    }
}
