using System.Text.Json.Serialization;

namespace MediaDatabase.Service.Configurations
{
    public class MediaDatabaseServiceConfig
    {
        [JsonPropertyName("info-storage-path")]
        public string InfoStoragePath { get; set; }
    }
}