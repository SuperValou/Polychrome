using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.TaskConfigs
{
    public class DumpExportsSetup
    {
        [JsonPropertyName("download")]
        public Download Download { get; set; }

        [JsonPropertyName("decompress")]
        public Decompress Decompress { get; set; }

        [JsonPropertyName("quick-filter")]
        public QuickFilter QuickFilter { get; set; }
    }
}