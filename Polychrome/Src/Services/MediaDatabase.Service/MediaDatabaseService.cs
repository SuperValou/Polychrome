using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using MediaDatabase.Service.Configurations;
using MediaDatabase.Service.DTOs;

namespace MediaDatabase.Service
{
    public class MediaDatabaseService : IMediaDatabaseService
    {
        private string _mediaStoragePath;
        private string _infoStoragePath;

        public bool IsInitialized { get; private set; }

        public Task Initialize(MediaDatabaseServiceConfig config)
        {
            return Task.Run(() =>
            {
                _mediaStoragePath = config.MediaStoragePath;
                _infoStoragePath = config.InfoStoragePath;

                IsInitialized = true;
            });
        }

        public Task<string> GetMediaId(string mediaFilePath)
        {
            return Task.Run(() =>
            {
                return mediaFilePath;
            });
        }

        public Task<MediaInfo> GetMediaInfo(string mediaId)
        {
            return Task.Run(() =>
            {
                return new MediaInfo();
            });
        }

        public async Task Update(string mediaId, MediaInfo mediaInfo)
        {
            // TODO: currently assumes mediaId is source file path
            string mediaInfoFileName = Path.GetFileNameWithoutExtension(mediaId);
            string outputFilePath = Path.Combine(_infoStoragePath, $"{mediaInfoFileName}.json");
            
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
