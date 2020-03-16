using System.Text.Json.Serialization;

namespace MediaDatabase.Service.Configurations
{
    public class MediaDatabaseServiceConfig
    {
        [JsonPropertyName("media-storage-path")]
        public string MediaStoragePath { get; set; }

        [JsonPropertyName("info-storage-path")]
        public string InfoStoragePath { get; set; }
    }
}