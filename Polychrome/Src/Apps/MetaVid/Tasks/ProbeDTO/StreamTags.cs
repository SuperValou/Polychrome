using System;
using System.Text.Json.Serialization;

namespace MetaVid.Tasks.ProbeDTO
{
    public class StreamTags
    {
        [JsonPropertyName("creation_time")]
        public string CreationTime { get; set; }

        [JsonPropertyName("language")]
        public string Language { get; set; }
    }
}