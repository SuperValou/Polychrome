using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TmdbService.Exports
{
    internal interface IExportManager
    {
        Task Download(ICollection<string> exportIds, DateTime exportDate, string outputFolder, bool force);
    }
}
