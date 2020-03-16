using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MediaDatabase.Service.Configurations;
using MediaDatabase.Service.DTOs;

namespace MediaDatabase.Service
{
    // TODO: currently assumes mediaId is source file name without extension
    public class MediaDatabaseService : IMediaDatabaseService
    {
        private string _mediaStoragePath;
        private string _infoStoragePath;

        public bool IsInitialized { get; private set; }

        public async Task Initialize(MediaDatabaseServiceConfig config)
        {
                _mediaStoragePath = config.MediaStoragePath;
                _infoStoragePath = config.InfoStoragePath;

                if (!Directory.Exists(_infoStoragePath))
                {
                    Directory.CreateDirectory(_infoStoragePath);
                }
                
                IsInitialized = true;
        }

        public Task<string> GetMediaId(string mediaFilePath)
        {
            return Task.Run(() =>
            {
                return Path.GetFileNameWithoutExtension(mediaFilePath);
            });
        }

        public async Task<MediaInfo> GetOrCreateMediaInfo(string mediaId)
        {
            string mediaInfoFileName = $"{mediaId}.json";
            string mediaInfoFilePath = Path.Combine(_mediaStoragePath, mediaInfoFileName);

            if (!File.Exists(mediaInfoFilePath))
            {
                return new MediaInfo();
            }

            MediaInfo mediaInfo;
            await using (var reader = File.OpenRead(mediaInfoFilePath))
            {
                mediaInfo = await JsonSerializer.DeserializeAsync<MediaInfo>(reader);
            }

            return mediaInfo;
        }

        public async Task UpdateMediaInfo(string mediaId, MediaInfo mediaInfo)
        {
            string outputFilePath = Path.Combine(_infoStoragePath, $"{mediaId}.json");
            
            await using (var writer = File.Create(outputFilePath))
            {
                var options = new JsonSerializerOptions() { WriteIndented = true };
                await JsonSerializer.SerializeAsync(writer, mediaInfo, options);
            }
        }

        public void Dispose()
        {
        }
    }
}
