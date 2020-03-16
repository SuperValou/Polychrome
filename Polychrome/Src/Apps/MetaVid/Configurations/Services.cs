using System.Text.Json.Serialization;
using MediaDatabase.Service.Configurations;

namespace MetaVid.Configurations
{
    public class Services
    {
        //[JsonPropertyName("tmdb-service-config")]
        //public TmdbServiceConfig TmdbServiceConfig { get; set; }

        [JsonPropertyName("media-database-service")]
        public MediaDatabaseServiceConfig MediaDatabaseServiceConfig { get; set; }
    }
}