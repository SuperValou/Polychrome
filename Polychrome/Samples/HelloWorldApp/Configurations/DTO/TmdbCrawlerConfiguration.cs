using System.Text.Json.Serialization;
using ApplicationCore.Configurations;

namespace HelloWorldApp.Configurations.DTO
{
    public class TmdbCrawlerConfiguration : IConfiguration
    {
        [JsonPropertyName("app")]
        public string App { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("download")]
        public Download Download { get; set; }

        [JsonPropertyName("decompress")]
        public Decompress Decompress { get; set; }

        [JsonPropertyName("crawl")]
        public Crawl Crawl { get; set; }

        [JsonPropertyName("graph")]
        public Graph Graph { get; set; }
    }
}