using System.Text.Json.Serialization;

namespace SwapFusion.Configurations
{
    public class MediaDatabaseService
    {
        [JsonPropertyName("database-root")]
        public string DatabaseRoot { get; set; }
    }
}