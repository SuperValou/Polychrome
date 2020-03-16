using System.Text.Json.Serialization;

namespace MediaDatabase.Service.DTOs
{
    public class VideoStream : Stream
    {
        [JsonPropertyName("width")]
        public int Width { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("fps")]
        public string Fps { get; set; }

        [JsonPropertyName("nb-frames")]
        public int NbFrames { get; set; }
    }
}