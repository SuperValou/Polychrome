﻿using System;
using System.IO;
using System.Threading.Tasks;
using LightLogs.API;
using LightLogs.Configs;

namespace LightLogs.Targets
{
    public class FileTarget : ITarget
    {
        private string _logFolder;
        private string _logFileName;
        
        private int _writeAttemps;
        private int _writeAttempsDelay;

        private string _archiveFolderName;

        public LogLevel MinLogLevel { get; private set; }

        public string LogFilePath { get; private set; }

        public void Initialize(FileTargetConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            _logFolder = config.LogFolder;
            _logFileName = config.LogFileName;
            LogFilePath = Path.Combine(_logFolder, _logFileName);

            MinLogLevel = config.MinLogLevel;

            _writeAttemps = config.WriteAttempts;
            _writeAttempsDelay = config.WriteAttemptsDelay;

            _archiveFolderName = config.ArchiveFolderName;

            if (!Directory.Exists(_logFolder))
            {
                Directory.CreateDirectory(_logFolder);
            }

            if (File.Exists(LogFilePath))
            {
                // TODO: move old log file to archive folder
            }

            File.WriteAllText(LogFilePath, string.Empty);
        }

        public async Task Write(LogLevel level, char[] log)
        {
            await using (FileStream stream = await OpenFileStream())
            {
                await using (var writer = new StreamWriter(stream))
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
                        FileStream fileStream = new FileStream(LogFilePath, FileMode.Append, FileAccess.Write, fileShare);
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
                        File.WriteAllText(LogFilePath, string.Empty);
                    }
                }
                catch (IOException)
                {
                    await Task.Delay(_writeAttempsDelay);
                }
            }

            throw new InvalidOperationException($"Unable to open filestream for \"{LogFilePath}\" after {_writeAttemps} attempts.");
        }
    }
}