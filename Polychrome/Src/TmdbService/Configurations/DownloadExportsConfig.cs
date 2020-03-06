using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TmdbService.Configurations
{
    public class DownloadExportsConfig
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("ids")]
        public List<string> Ids { get; set; }

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