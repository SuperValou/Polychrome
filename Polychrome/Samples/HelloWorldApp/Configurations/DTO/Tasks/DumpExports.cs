using System.Text.Json.Serialization;

namespace HelloWorldApp.Configurations.DTO
{
    public class DumpExports
    {
        [JsonPropertyName("download")]
        public Download Download { get; set; }

        [JsonPropertyName("decompress")]
        public Decompress Decompress { get; set; }

        [JsonPropertyName("quick-filter")]
        public Graph QuickFilter { get; set; }
    }
}