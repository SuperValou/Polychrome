using System;
using System.Collections.Generic;
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

        public Task Initialize(MediaDatabaseServiceConfig config)
        {
            return Task.Run(() =>
            {
                _mediaStoragePath = config.MediaStoragePath;
                _infoStoragePath = config.InfoStoragePath;

                if (!Directory.Exists(_infoStoragePath))
                {
                    Directory.CreateDirectory(_infoStoragePath);
                }

                IsInitialized = true;
            });
        }

        public Task<string> GetMediaId(string mediaFilePath)
        {
            return Task.Run(() =>
            {
                return Path.GetFileNameWithoutExtension(mediaFilePath);
            });
        }

        public Task<ICollection<string>> GetAllMediaIds()
        {
            return Task.Run(() =>
            {
                ICollection<string> mediaIds = new List<string>();
                foreach (var mediaFile in Directory.EnumerateDirectories(_mediaStoragePath, "*.mp4",  SearchOption.AllDirectories))
                {
                    mediaIds.Add(Path.GetFileNameWithoutExtension(mediaFile));
                }

                return mediaIds;
            });
        }

        public async Task<MediaInfo> GetOrCreateMediaInfo(string mediaId)
        {
            string mediaInfoFileName = $"{mediaId}.json";
            string mediaInfoFilePath = Path.Combine(_mediaStoragePath, mediaInfoFileName);

            MediaInfo mediaInfo;
            if (!File.Exists(mediaInfoFilePath))
            {
                 mediaInfo = new MediaInfo();
                 mediaInfo = await UpdateMediaInfo(mediaId, mediaInfo);
            }
            
            await using (var reader = File.OpenRead(mediaInfoFilePath))
            {
                mediaInfo = await JsonSerializer.DeserializeAsync<MediaInfo>(reader);
            }

            return mediaInfo;
        }

        public async Task<MediaInfo> GetMediaInfo(string mediaId)
        {
            string mediaInfoFileName = $"{mediaId}.json";
            string mediaInfoFilePath = Path.Combine(_mediaStoragePath, mediaInfoFileName);

            await using (var reader = File.OpenRead(mediaInfoFilePath))
            {
                return await JsonSerializer.DeserializeAsync<MediaInfo>(reader);
            }
        }

        public async Task<MediaInfo> UpdateMediaInfo(string mediaId, MediaInfo mediaInfo)
        {
            string outputFilePath = Path.Combine(_infoStoragePath, $"{mediaId}.json");
            
            await using (var writer = File.Create(outputFilePath))
            {
                var options = new JsonSerializerOptions() { WriteIndented = true };
                await JsonSerializer.SerializeAsync(writer, mediaInfo, options);
            }

            return mediaInfo;
        }

        public void Dispose()
        {
        }
    }
}
