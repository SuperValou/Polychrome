using System;

namespace SimpleLogs.Configs
{
    public class FileTargetConfig
    {
        private string _logFolder;
        private string _logFileName;

        public string LogFolder
        {
            get => _logFolder;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"New value of {nameof(LogFolder)} cannot be null or empty.");
                }

                _logFolder = value;
            }
        }

        public string LogFileName
        {
            get => _logFileName;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException($"New value of {nameof(LogFileName)} cannot be null or empty.");
                }

                _logFileName = value;
            }
        }

        public string ArchiveFolderName { get; set; } = "LogArchives";

        public int WriteAttempts { get; set; } = 10;
        public int WriteAttemptsDelay { get; set; } = 200;

        public FileTargetConfig()
        {
            LogFolder = AppDomain.CurrentDomain.BaseDirectory;
            LogFileName = $"{AppDomain.CurrentDomain.FriendlyName}.txt";
        }
    }
}