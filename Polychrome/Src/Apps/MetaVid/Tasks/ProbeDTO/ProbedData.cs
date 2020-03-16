using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MetaVid.Tasks.ProbeDTO
{
    public class ProbedData
    {
        [JsonPropertyName("streams")]
        public List<Stream> Streams { get; set; }

        [JsonPropertyName("format")]
        public Format Format { get; set; }
    }
}