using Kernel;
using System;
using System.Threading.Tasks;
using TaskSystem;
using Tmdb.Service.Configurations;
using Tmdb.Service.Exports;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace Tmdb.Service
{
    public class TmdbServiceClient : ITmdbService
    {
        private readonly ILogger _logger;
        private readonly ITaskManager taskManager;
        private IExportManager _exportManager;

        public TmdbServiceClient(ILogger logger, ITaskManager taskManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.taskManager = taskManager ?? throw new ArgumentNullException(nameof(taskManager));
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
