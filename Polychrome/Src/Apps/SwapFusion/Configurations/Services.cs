using System.Text.Json.Serialization;

namespace SwapFusion.Configurations
{
    public class Services
    {
        [JsonPropertyName("media-database-service")]
        public MediaDatabaseService MediaDatabaseService { get; set; }
    }
}