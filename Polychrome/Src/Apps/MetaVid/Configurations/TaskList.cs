using System.Text.Json.Serialization;

namespace MetaVid.Configurations
{
    public class TaskList
    {
        [JsonPropertyName("probe-task")]
        public ProbeTaskSetup ProbeTaskSetup { get; set; }
    }
}