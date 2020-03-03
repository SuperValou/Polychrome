using System;
using System.Text.Json.Serialization;

namespace TmdbService.Configurations
{
    public class TmdbServiceConfig
    {
        [JsonPropertyName("exports-root")]
        public Uri ExportsRoot { get; set; }
    }
}
