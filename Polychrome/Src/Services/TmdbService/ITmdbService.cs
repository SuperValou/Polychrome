using System;
using System.Threading.Tasks;
using Kernel;
using Tmdb.Service.Configurations;

namespace Tmdb.Service
{
    public interface ITmdbService : IService
    {
        Task Initialize(TmdbServiceConfig config);

        /// <summary>
        /// Download a daily file export from Tmdb. See: https://developers.themoviedb.org/3/getting-started/daily-file-exports
        /// </summary>
        /// <param name="idType">movie_ids, tv_series_ids, person_ids, collection_ids, tv_network_ids, keyword_ids, production_company_ids</param>
        /// <param name="date">Date of the export</param>
        /// <param name="outputDirectory">Directory to download the export to</param>
        /// <param name="force">Force download even, if the destination file already exists</param>
        /// <returns>Path to the downloaded file</returns>
        Task<string> DownloadExport(string idType, DateTime date, string outputDirectory, bool force);
    }
}