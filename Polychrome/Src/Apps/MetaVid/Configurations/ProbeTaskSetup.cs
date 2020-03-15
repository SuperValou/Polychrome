using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetaVid.Configurations
{
    public class ProbeTaskSetup
    {
        [JsonPropertyName("ffprobe-path")]
        public string FfProbePath { get; set; }

        [JsonPropertyName("source-folder")]
        public string SourceFolder { get; set; }

        [JsonPropertyName("source-extensions")]
        public List<string> SourceExtensions { get; set; }
    }
}