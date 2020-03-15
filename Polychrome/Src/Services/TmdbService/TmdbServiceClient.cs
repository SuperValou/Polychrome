using Kernel;
using System;
using System.Threading.Tasks;
using Tmdb.Service.Configurations;
using Tmdb.Service.Exports;

namespace Tmdb.Service
{
    public class TmdbServiceClient : ITmdbService
    {
        private readonly ILogger _logger;
        private IExportManager _exportManager;

        public TmdbServiceClient(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize(TmdbServiceConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var subLogger = _logger.CreateSubLogger(nameof(ExportManager));
            _exportManager = new ExportManager(subLogger, config.ExportsRoot);
        }

        public async Task DumpExports(DownloadExportsConfig config)
        {

            if (config.Date == "default")
            {
                // blablabla
            }

            await _exportManager.Download(new string[0], DateTime.Now, string.Empty, force: false);


            throw new NotImplementedException();
        }
    }
}
