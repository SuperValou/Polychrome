using System.Text.Json.Serialization;

namespace MetaVid.Configurations
{
    public class TaskList
    {
        [JsonPropertyName("working-directory")]
        public string WorkingDirectory { get; set; }

        [JsonPropertyName("probe-task")]
        public ProbeTaskSetup ProbeTaskSetup { get; set; }
    }
}