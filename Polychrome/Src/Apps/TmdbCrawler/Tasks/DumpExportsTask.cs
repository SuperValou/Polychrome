using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using Tmdb.Service;
using TmdbCrawler.Configurations.TaskConfigs;

namespace TmdbCrawler.Tasks
{
    public class DumpExportsTask : ITask
    {
        private readonly DumpExportsSetup _setup;
        private readonly string _workingDirectory;
        private readonly ITmdbService _tmdbService;

        public DumpExportsTask(DumpExportsSetup setup, string workingDirectory, ITmdbService tmdbService)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            _tmdbService = tmdbService ?? throw new ArgumentNullException(nameof(tmdbService));
        }

        public async Task Execute()
        {
            // download
            DateTime date = DateTime.Parse(_setup.Download.Date);

            ICollection<string> exportedArchivePaths = new List<string>();
            foreach (string idType in _setup.Download.IdTypes)
            {
                // TODO: cache?
                string exportPath =  await _tmdbService.DownloadExport(idType, date, _workingDirectory, _setup.Download.Force);
                exportedArchivePaths.Add(exportPath);
            }

            // extract
            foreach (string archivePath in exportedArchivePaths)
            {
                FileInfo archiveFileInfo = new FileInfo(archivePath); // "movie_ids_04_28_2017.json.gz"
                string decompressedArchivePath = Path.GetFileNameWithoutExtension(archiveFileInfo.FullName); // "movie_ids_04_28_2017.json"

                if (File.Exists(decompressedArchivePath))
                {
                    if (_setup.Decompress.Force)
                    {
                        File.Delete(decompressedArchivePath);
                    }
                    else
                    {
                        continue;
                    }
                }

                await using (Stream archiveStream = archiveFileInfo.OpenRead())
                {
                    await using (Stream decompressedArchiveStream = File.Create(decompressedArchivePath))
                    {
                        await using (GZipStream decompressionStream = new GZipStream(archiveStream, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedArchiveStream);
                        }
                    }
                }
            }
            
            // quick filter
        }
    }
}
