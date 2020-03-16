using System.Text.Json.Serialization;
using MediaDatabase.Service.Configurations;

namespace SwapFusion.Configurations
{
    public class Services
    {
        [JsonPropertyName("media-database-service")]
        public MediaDatabaseServiceConfig MediaDatabaseServiceConfig { get; set; }
    }
}