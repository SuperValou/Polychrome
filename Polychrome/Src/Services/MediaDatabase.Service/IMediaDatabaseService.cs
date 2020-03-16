using System.Threading.Tasks;
using Kernel;
using MediaDatabase.Service.DTOs;

namespace MediaDatabase.Service
{
    public interface IMediaDatabaseService : IService
    {
        Task Initialize();

        Task<string> GetMediaId(string mediaFilePath);

        Task<MediaInfo> GetMediaInfo(string mediaId);

        Task Update(string mediaId, MediaInfo mediaInfo);
    }
}