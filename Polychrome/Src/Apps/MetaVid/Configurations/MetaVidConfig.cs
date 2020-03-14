using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetaVid.Configurations
{
    public partial class MetaVidConfig
    {
        [JsonPropertyName("app-name")]
        public string AppName { get; set; }

        [JsonPropertyName("app-version")]
        public string AppVersion { get; set; }

        [JsonPropertyName("services")]
        public Services Services { get; set; }

        [JsonPropertyName("ffprobe-path")]
        public string FfprobPath { get; set; }

        [JsonPropertyName("source-folder")]
        public string SourceFolder { get; set; }

        [JsonPropertyName("source-extensions")]
        public List<string> SourceExtensions { get; set; }

        [JsonPropertyName("destination-folder")]
        public string DestinationFolder { get; set; }
    }
}
