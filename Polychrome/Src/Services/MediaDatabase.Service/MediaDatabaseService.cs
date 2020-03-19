using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MediaDatabase.Service.Configurations;
using MediaDatabase.Service.DTOs;

namespace MediaDatabase.Service
{
    // TODO: strill a crappy file-based database
    public class MediaDatabaseService : IMediaDatabaseService
    {
        private const string IndexFileName = "index.txt";
        private Dictionary<string, string> _index = new Dictionary<string, string>();
                
        private string _infoStoragePath;

        public bool IsInitialized { get; private set; }

        public async Task Initialize(MediaDatabaseServiceConfig config)
        {            
            _infoStoragePath = config.InfoStoragePath;

            if (!Directory.Exists(_infoStoragePath))
            {
                Directory.CreateDirectory(_infoStoragePath);
            }

            await ReadIndex();

            IsInitialized = true;
        }

        public Task<ICollection<string>> GetAllMediaIds()
        {
            return Task.Run(() =>
            {
                ICollection<string> mediaIds = _index.Keys.ToList();
                return mediaIds;
            });
        }

        public async Task<MediaInfo> CreateOrGetMediaInfoFromFile(string mediaFilePath)
        {
            if (!File.Exists(mediaFilePath))
            {
                throw new InvalidOperationException($"Media doesn't exist at '{mediaFilePath}'.");
            }

            string mediaId = GenerateMediaId(mediaFilePath);

            // file
            string mediaInfoFileName = $"{mediaId}.json";
            string mediaInfoFilePath = Path.Combine(_infoStoragePath, mediaInfoFileName);

            MediaInfo mediaInfo;
            if (File.Exists(mediaInfoFilePath))
            {
                await using (var reader = File.OpenRead(mediaInfoFilePath))
                {
                    mediaInfo = await JsonSerializer.DeserializeAsync<MediaInfo>(reader);
                }
            }
            else
            {
                mediaInfo = new MediaInfo()
                {
                    MediaId = mediaId                    
                };

                await using (var writer = File.OpenWrite(mediaInfoFilePath))
                {
                    await JsonSerializer.SerializeAsync(writer, mediaInfo);
                }
            }

            _index[mediaId] = mediaFilePath;

            return mediaInfo;
        }

        public async Task<MediaInfo> GetMediaInfo(string mediaId)
        {
            string mediaInfoFileName = $"{mediaId}.json";
            string mediaInfoFilePath = Path.Combine(_infoStoragePath, mediaInfoFileName);

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

        public Task DeleteMediaInfo(string mediaId, MediaInfo mediaInfo)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            WriteIndex(); // Don't do this at home kids
        }

        private string GenerateMediaId(string mediaFilePath)
        {
            // crappy crap
            var builder = new StringBuilder();

            string filename = Path.GetFileNameWithoutExtension(mediaFilePath);
            builder.Append(filename);

            var fileInfo = new FileInfo(mediaFilePath);
            builder.Append(fileInfo.Length.ToString());
            
            return builder.ToString();
        }

        private async Task ReadIndex()
        {
            string indexFilePath = Path.Combine(_infoStoragePath, IndexFileName);

            if (!File.Exists(indexFilePath))
            {
                return;
            }

            using (var reader = File.OpenText(indexFilePath))
            {
                while (!reader.EndOfStream)
                {
                    string line = await reader.ReadLineAsync();
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    // #yolo
                    int splitCharIndex = line.IndexOf(':');
                    string mediaId = line.Remove(splitCharIndex);
                    string mediaInfoPath = line.Substring(splitCharIndex + 1);

                    if (File.Exists(mediaInfoPath)) // worst idea
                    {
                        _index.Add(mediaId, mediaInfoPath);
                    }                    
                }
            }
        }

        private void WriteIndex()
        {
            string indexFilePath = Path.Combine(_infoStoragePath, IndexFileName);

            using (var writer = File.CreateText(indexFilePath))
            {
                foreach (var kvp in _index)
                {
                    writer.WriteLine($"{kvp.Key}:{kvp.Value}"); // yeehaa
                }
            }
        }
    }
}
