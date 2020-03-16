using System.Text.Json.Serialization;

namespace SwapFusion.Configurations
{
    public class GenerateSwapsTaskSetup
    {
        [JsonPropertyName("ffmpeg-path")]
        public string FfmpegPath { get; set; }

        [JsonPropertyName("swap-media-count")]
        public long SwapMediaCount { get; set; }

        [JsonPropertyName("swaps-per-media")]
        public long SwapsPerMedia { get; set; }
    }
}