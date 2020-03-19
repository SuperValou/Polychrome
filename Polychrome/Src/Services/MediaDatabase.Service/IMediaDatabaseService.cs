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
        
        Task<ICollection<string>> GetAllMediaIds();

        Task<MediaInfo> CreateOrGetMediaInfoFromFile(string mediaFilePath);

        Task<MediaInfo> GetMediaInfo(string mediaId);

        Task<MediaInfo> UpdateMediaInfo(string mediaId, MediaInfo mediaInfo);

        Task DeleteMediaInfo(string mediaId, MediaInfo mediaInfo);

    }
}