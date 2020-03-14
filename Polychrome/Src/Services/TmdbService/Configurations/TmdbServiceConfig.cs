using System;
using System.Text.Json.Serialization;

namespace Tmdb.Service.Configurations
{
    public class TmdbServiceConfig
    {
        [JsonPropertyName("exports-root")]
        public Uri ExportsRoot { get; set; }
    }
}
