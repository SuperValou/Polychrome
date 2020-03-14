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

        [JsonPropertyName("ffmpeg-path")]
        public string FfmpegPath { get; set; }

        [JsonPropertyName("source-folder")]
        public string SourceFolder { get; set; }

        [JsonPropertyName("destination-folder")]
        public string DestinationFolder { get; set; }
    }
}
