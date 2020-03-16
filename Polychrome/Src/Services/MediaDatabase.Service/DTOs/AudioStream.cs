using System.Text.Json.Serialization;

namespace MediaDatabase.Service.DTOs
{
    public class AudioStream : Stream
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("nb-channels")]
        public int NbChannels { get; set; }

        // "stereo"
        [JsonPropertyName("channels-layout-name")]
        public string ChannelLayoutName { get; set; }

        // 48000
        [JsonPropertyName("sample-rate")]
        public int SampleRate { get; set; }
    }
}