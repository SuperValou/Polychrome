using System.Text.Json.Serialization;

namespace MediaDatabase.Service.DTOs
{
    public class Stream
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("start_time")]
        public float StartTime { get; set; }

        [JsonPropertyName("duration")]
        public float Duration { get; set; }

        // "h264", "aac", "dvd_subtitle"
        [JsonPropertyName("codec-name")]
        public string CodecName { get; set; }

        // "avc1", "mp4a", "mp4s"
        [JsonPropertyName("codec-tag")]
        public string CodecTag { get; set; }
    }
}