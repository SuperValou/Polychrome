using System;

namespace LightLogs.Configs
{
    public static class DefaultConfigFactory
    {
        public static ConsoleTargetConfig GetDefaultConsoleTargetConfig()
        {
            var config = new ConsoleTargetConfig()
            {
                MinLogLevel = LogLevel.Info,
                TraceColor = ConsoleColor.DarkGray,
                DebugColor = ConsoleColor.Gray,
                InfoColor = ConsoleColor.White,
                WarningColor = ConsoleColor.DarkYellow,
                ErrorColor = ConsoleColor.Red,
                FatalColor = ConsoleColor.Red,
            };

            return config;
        }

        public static FileTargetConfig GetDefaultFileTargetConfig()
        {
            string defaultLogFolder = AppDomain.CurrentDomain.BaseDirectory;
            string defaultLogFileName = $"{AppDomain.CurrentDomain.FriendlyName}.txt";

            var config = new FileTargetConfig()
            {
                MinLogLevel = LogLevel.Debug,
                LogFolder = defaultLogFolder,
                ArchiveFolderName = "LogArchives",
                LogFileName = defaultLogFileName,
                WriteAttempts = 10,
                WriteAttemptsDelay = 200
            };

            return config;
        }
    }
}