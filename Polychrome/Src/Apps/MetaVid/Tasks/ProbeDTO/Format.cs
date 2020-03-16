using System.Text.Json.Serialization;

namespace MetaVid.Tasks.ProbeDTO
{
    public class Format
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; }

        [JsonPropertyName("nb_streams")]
        public int NbStreams { get; set; }

        [JsonPropertyName("format_long_name")]
        public string FormatLongName { get; set; }

        [JsonPropertyName("start_time")]
        public string StartTime { get; set; }

        [JsonPropertyName("duration")]
        public string Duration { get; set; }

        [JsonPropertyName("size")]
        public string Size { get; set; }

        [JsonPropertyName("bit_rate")]
        public string BitRate { get; set; }

        [JsonPropertyName("tags")]
        public FormatTags Tags { get; set; }
    }
}