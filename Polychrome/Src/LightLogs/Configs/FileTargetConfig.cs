namespace LightLogs.Configs
{
    public class FileTargetConfig
    {
        public string LogFolder { get; set; }
       
        public string LogFileName { get; set; }

        public string ArchiveFolderName { get; set; }

        public int WriteAttempts { get; set; }
        public int WriteAttemptsDelay { get; set; }
    }
}