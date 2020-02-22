using System;

namespace LightLogs.Configs
{
    public class ConsoleTargetConfig
    {
        public ConsoleColor TraceColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor DebugColor { get; set; } = ConsoleColor.Gray;
        public ConsoleColor InfoColor { get; set; } = ConsoleColor.White;
        public ConsoleColor WarningColor { get; set; } = ConsoleColor.DarkYellow;
        public ConsoleColor ErrorColor { get; set; } = ConsoleColor.Red;
        public ConsoleColor FatalColor { get; set; } = ConsoleColor.Red;
    }
}