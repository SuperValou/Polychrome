using System.Text.Json.Serialization;
using Tmdb.Service.Configurations;

namespace TmdbCrawler.Configurations
{
    public class Services
    {
        [JsonPropertyName("tmdb-service-config")]
        public TmdbServiceConfig TmdbServiceConfig { get; set; }
    }
}