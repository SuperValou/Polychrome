using System;
using System.IO;
using System.Threading.Tasks;
using SimpleLogs.Configs;

namespace SimpleLogs.Targets
{
    internal class FileTarget : ITarget
    {
        private string _logFolder;
        private string _logFileName;
        private string _logFilePath;

        private int _writeAttemps;
        private int _writeAttempsDelay;

        private string _archiveFolderName;
        
        public FileTarget()
        {
        }

        public void Initialize(FileTargetConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _logFolder = config.LogFolder;
            _logFileName = config.LogFileName;
            _logFilePath = Path.Combine(_logFolder, _logFileName);

            _writeAttemps = config.WriteAttempts;
            _writeAttempsDelay = config.WriteAttemptsDelay;

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            if (File.Exists(_logFilePath))
            {
                _archiveFolderName = config.ArchiveFolderName;
            }

            File.WriteAllText(_logFilePath, string.Empty);
        }

        

        public async Task Write(LogLevel level, char[] log)
        {
            using (FileStream stream = await OpenFileStream())
            {
                using (var writer = new StreamWriter(stream))
                {
                    await writer.WriteAsync(log);
                }
            }
        }

        public void Dispose()
        {
        }


        private async Task<FileStream> OpenFileStream()
        {
            for (int i = 0; i < _writeAttemps; ++i)
            {
                try
                {
                    FileShare fileShare = FileShare.ReadWrite | FileShare.Delete;
                    try
                    {
                        FileStream fileStream = new FileStream(_logFilePath, FileMode.Append, FileAccess.Write, fileShare);
                        return fileStream;
                    }
                    catch (DirectoryNotFoundException)
                    {
                        // directory was deleted
                        Directory.CreateDirectory(_logFolder);
                    }
                    catch (FileNotFoundException)
                    {
                        // file was deleted
                        File.WriteAllText(_logFilePath, string.Empty);
                    }
                }
                catch (IOException)
                {
                    await Task.Delay(_writeAttempsDelay);
                }
            }

            throw new InvalidOperationException("Should not be reached.");
        }


    }
}