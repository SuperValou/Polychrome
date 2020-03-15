using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.Tasks
{
    public class Download
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("id-types")]
        public List<string> IdTypes { get; set; }

        [JsonPropertyName("output-filename")]
        public string OutputFilename { get; set; }

        [JsonPropertyName("output-folder")]
        public string OutputFolder { get; set; }

        [JsonPropertyName("skip-existing")]
        public bool SkipExisting { get; set; }

        [JsonPropertyName("keep-clean")]
        public bool KeepClean { get; set; }
    }
}