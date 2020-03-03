using System.Threading.Tasks;
using TmdbService.Configurations;

namespace TmdbService
{
    public interface ITmdbService
    {
        Task DumpExports(DownloadExportsConfig config);
    }
}