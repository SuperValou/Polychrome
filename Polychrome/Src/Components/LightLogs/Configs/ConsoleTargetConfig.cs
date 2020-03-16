using System;

namespace LightLogs.Configs
{
    public class ConsoleTargetConfig
    {
        public LogLevel MinLogLevel { get; set; }

        public ConsoleColor TraceColor { get; set; }
        public ConsoleColor DebugColor { get; set; }
        public ConsoleColor InfoColor { get; set; }
        public ConsoleColor WarningColor { get; set; }
        public ConsoleColor ErrorColor { get; set; }
        public ConsoleColor FatalColor { get; set; }
    }
}