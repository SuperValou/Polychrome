using System.Collections.Generic;
using System.Threading.Tasks;
using Kernel;
using MediaDatabase.Service.Configurations;
using MediaDatabase.Service.DTOs;

namespace MediaDatabase.Service
{
    public interface IMediaDatabaseService : IService
    {
        Task Initialize(MediaDatabaseServiceConfig config);

        Task<string> GetMediaId(string mediaFilePath);

        Task<ICollection<string>> GetAllMediaIds();

        Task<MediaInfo> GetOrCreateMediaInfo(string mediaId);

        Task<MediaInfo> GetMediaInfo(string mediaId);

        Task<MediaInfo> UpdateMediaInfo(string mediaId, MediaInfo mediaInfo);

        
    }
}