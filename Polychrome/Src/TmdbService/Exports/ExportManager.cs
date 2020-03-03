using Kernel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TmdbService.Exports
{
    internal class ExportManager : IExportManager
    {
        private const string ExportFileName = "{id}_{date:MM}_{date:DD}_{date:YYYY}.json.gz";

        public ExportManager(ILogger logger, Uri exportsRoot)
        {

        }

        public Task Download(ICollection<string> exportIds, DateTime exportDate, string outputFolder, bool force)
        {
            if (exportIds == null)
            {
                throw new ArgumentNullException(nameof(exportIds));
            }

            foreach (string exportId in exportIds)
            {

            }

            throw new NotImplementedException();
        }
    }
}
