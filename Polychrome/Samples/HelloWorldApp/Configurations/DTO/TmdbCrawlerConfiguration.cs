using System.Text.Json.Serialization;
using ApplicationCore.Configurations;
using TmdbService.Configurations;

namespace HelloWorldApp.Configurations.DTO
{
    public class TmdbCrawlerConfiguration : IConfiguration
    {
        [JsonPropertyName("app")]
        public string AppName { get; set; }

        [JsonPropertyName("version")]
        public string AppVersion { get; set; }

        [JsonPropertyName("download")]
        public DownloadExportsConfig Download { get; set; }

        [JsonPropertyName("decompress")]
        public Decompress Decompress { get; set; }

        [JsonPropertyName("crawl")]
        public Crawl Crawl { get; set; }

        [JsonPropertyName("graph")]
        public Graph Graph { get; set; }
    }
}