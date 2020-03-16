using System.Text.Json.Serialization;

namespace MediaDatabase.Service.DTOs
{
    public class SubtitleStream : Stream
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}