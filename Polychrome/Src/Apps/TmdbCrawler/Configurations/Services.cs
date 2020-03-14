using System.Text.Json.Serialization;
using Tmdb.Service.Configurations;

namespace HelloWorldApp.Configurations.DTO
{
    public class Services
    {
        [JsonPropertyName("tmdb-service-config")]
        public TmdbServiceConfig TmdbServiceConfig { get; set; }
    }
}