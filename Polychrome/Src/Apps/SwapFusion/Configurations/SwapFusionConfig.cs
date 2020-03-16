using System.Text.Json.Serialization;
using ApplicationCore.Configurations;

namespace SwapFusion.Configurations
{
    public class SwapFusionConfig : IConfiguration
    {
        [JsonPropertyName("app-name")]
        public string AppName { get; set; }

        [JsonPropertyName("app-version")]
        public string AppVersion { get; set; }

        [JsonPropertyName("services")]
        public Services Services { get; set; }

        [JsonPropertyName("task-list")]
        public TaskList TaskList { get; set; }
    }
}