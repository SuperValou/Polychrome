using System.Threading.Tasks;
using Tmdb.Service.Configurations;

namespace Tmdb.Service
{
    public interface ITmdbService
    {
        Task DumpExports(DownloadExportsConfig config);
    }
}