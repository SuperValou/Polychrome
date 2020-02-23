using System;
using System.Threading.Tasks;
using LightLogs.API;
using LightLogs.Configs;

namespace LightLogs.Targets
{
    public class ConsoleTarget : ITarget
    {
        private ConsoleTargetConfig _consoleTargetConfig;

        public ConsoleTarget()
        {
        }

        public void Initialize(ConsoleTargetConfig consoleTargetConfig)
        {
            _consoleTargetConfig = consoleTargetConfig ?? throw new ArgumentNullException(nameof(consoleTargetConfig));
        }

        public Task Write(LogLevel level, char[] log)
        {
            return Task.Run(() =>
            {
                ConsoleColor oldColor = Console.ForegroundColor;
                
                switch (level)
                {
                    case LogLevel.Trace:
                        Console.ForegroundColor = _consoleTargetConfig.TraceColor;
                        break;

                    case LogLevel.Debug:
                        Console.ForegroundColor = _consoleTargetConfig.DebugColor;
                        break;

                    case LogLevel.Info:
                        Console.ForegroundColor = _consoleTargetConfig.InfoColor;
                        break;

                    case LogLevel.Warning:
                        Console.ForegroundColor = _consoleTargetConfig.WarningColor;
                        break;

                    case LogLevel.Error:
                        Console.Error.Write(log);
                        Console.ForegroundColor = _consoleTargetConfig.ErrorColor;                        
                        break;

                    case LogLevel.Fatal:
                        Console.Error.Write(log);
                        Console.ForegroundColor = _consoleTargetConfig.FatalColor;
                        break;

                    default:                        
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