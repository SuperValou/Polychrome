using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.TaskConfigs
{
    public class Filter
    {
        [JsonPropertyName("property")]
        public string Property { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("operator")]
        public string Operator { get; set; }

        [JsonPropertyName("value")]
        public object Value { get; set; }
    }
}