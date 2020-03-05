using System.Text.Json.Serialization;
using TmdbService.Configurations;

namespace HelloWorldApp.Configurations.DTO
{
    public class Services
    {
        [JsonPropertyName("tmdb-service-config")]
        public TmdbServiceConfig TmdbServiceConfig { get; set; }
    }
}