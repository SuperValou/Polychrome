using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.TaskConfigs
{
    public class Download
    {
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("id-types")]
        public List<string> IdTypes { get; set; }

        [JsonPropertyName("force")]
        public bool Force { get; set; }
    }
}