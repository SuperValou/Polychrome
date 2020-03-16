using System;
using System.Threading.Tasks;
using LightLogs.API;
using LightLogs.Configs;

namespace LightLogs.Targets
{
    public class ConsoleTarget : ITarget
    {
        private ConsoleColor _traceColor;
        private ConsoleColor _debugColor;
        private ConsoleColor _infoColor;
        private ConsoleColor _warningColor;
        private ConsoleColor _errorColor;
        private ConsoleColor _fatalColor;

        public LogLevel MinLogLevel { get; private set; }

        public void Initialize(ConsoleTargetConfig consoleTargetConfig)
        {
            if (consoleTargetConfig == null)
            {
                throw new ArgumentNullException(nameof(consoleTargetConfig));
            }

            MinLogLevel = consoleTargetConfig.MinLogLevel;

            _traceColor = consoleTargetConfig.TraceColor;
            _debugColor = consoleTargetConfig.DebugColor;
            _infoColor = consoleTargetConfig.InfoColor;
            _warningColor = consoleTargetConfig.WarningColor;
            _errorColor = consoleTargetConfig.ErrorColor;
            _fatalColor = consoleTargetConfig.FatalColor;
        }

        public Task Write(LogLevel level, char[] log)
        {
            return Task.Run(() =>
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                
                switch (level)
                {
                    case LogLevel.Trace:
                        Console.ForegroundColor = _traceColor;
                        break;

                    case LogLevel.Debug:
                        Console.ForegroundColor = _debugColor;
                        break;

                    case LogLevel.Info:
                        Console.ForegroundColor = _infoColor;
                        break;

                    case LogLevel.Warning:
                        Console.ForegroundColor = _warningColor;
                        break;

                    case LogLevel.Error:
                        Console.ForegroundColor = _errorColor;                        
                        break;

                    case LogLevel.Fatal:
                        Console.ForegroundColor = _fatalColor;
                        break;
                }

                Console.Write(log);
                Console.ForegroundColor = oldColor;
            });
        }

        public void Dispose()
        {
        }
    }
}