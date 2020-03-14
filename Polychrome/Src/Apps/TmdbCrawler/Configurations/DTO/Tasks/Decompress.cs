using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace HelloWorldApp.Configurations.DTO.Tasks
{
    public class Decompress
    {
        [JsonPropertyName("source-files")]
        public List<string> SourceFiles { get; set; }

        [JsonPropertyName("output-folder")]
        public string OutputFolder { get; set; }

        [JsonPropertyName("skip-existing")]
        public bool SkipExisting { get; set; }

        [JsonPropertyName("keep-clean")]
        public bool KeepClean { get; set; }
    }
}