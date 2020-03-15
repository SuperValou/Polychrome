using System.Text.Json.Serialization;

namespace TmdbCrawler.Configurations.TaskConfigs
{
    public class Decompress
    {
        [JsonPropertyName("force")]
        public bool Force { get; set; }
    }
}