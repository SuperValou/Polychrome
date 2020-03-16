using System;
using System.Text.Json.Serialization;

namespace MetaVid.Tasks.ProbeDTO
{
    public class FormatTags
    {
        [JsonPropertyName("creation_time")]
        public string CreationTime { get; set; }

        [JsonPropertyName("encoder")]
        public string Encoder { get; set; }
    }
}