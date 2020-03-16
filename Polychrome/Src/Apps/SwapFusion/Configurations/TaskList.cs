using System.Text.Json.Serialization;

namespace SwapFusion.Configurations
{
    public class TaskList
    {
        [JsonPropertyName("working-directory")]
        public string WorkingDirectory { get; set; }

        [JsonPropertyName("generate-swaps")]
        public GenerateSwapsTaskSetup GenerateSwapsTaskSetup { get; set; }
    }
}