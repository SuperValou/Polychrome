using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.TaskConfigs
{
    public class TasksToRun
    {
        [JsonPropertyName("dump-exports")]
        public DumpExports DumpExports { get; set; }

        [JsonPropertyName("crawl")]
        public Crawl Crawl { get; set; }

        [JsonPropertyName("graph")]
        public Graph Graph { get; set; }
    }
}