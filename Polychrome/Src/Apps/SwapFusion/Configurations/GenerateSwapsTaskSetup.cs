using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SwapFusion.Configurations
{
    public class GenerateSwapsTaskSetup
    {
        [JsonPropertyName("ffmpeg-path")]
        public string FfmpegPath { get; set; }

        [JsonPropertyName("swap-duration")]
        public int SwapDuration { get; set; }

        [JsonPropertyName("couple-count")]
        public int CoupleCount { get; set; }

        [JsonPropertyName("swaps-per-couple")]
        public int SwapsPerCouple { get; set; }

        [JsonPropertyName("video-media-ids")]
        public List<string> VideoMediaIds { get; set; }

        [JsonPropertyName("audio-media-ids")]
        public List<string> AudioMediaIds { get; set; }

        [JsonPropertyName("audio-language")]
        public string AudioLanguage { get; set; }
    }
}