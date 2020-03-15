using System.Text.Json.Serialization;
using ApplicationCore.Configurations;
using TmdbCrawler.Configurations.Tasks;

namespace TmdbCrawler.Configurations
{
    public class TmdbCrawlerConfiguration : IConfiguration
    {
        [JsonPropertyName("app-name")]
        public string AppName { get; set; }

        [JsonPropertyName("app-version")]
        public string AppVersion { get; set; }

        [JsonPropertyName("services")]
        public Services Services { get; set; }

        [JsonPropertyName("tasks-to-run")]
        public TasksToRun TasksToRun { get; set; }
    }
}