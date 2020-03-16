using LightLogs;

namespace ApplicationCore.ArgParsing
{
    public class AppArgs
    {
        public string ConfigPath { get; set; } = string.Empty;
        public LogLevel? MinLogLevel { get; set; } = null;
        public bool DisableLogs { get; set; }
    }
}