using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tmdb.Service.Exports
{
    internal interface IExportManager
    {
        Task Download(ICollection<string> exportIds, DateTime exportDate, string outputFolder, bool force);
    }
}
