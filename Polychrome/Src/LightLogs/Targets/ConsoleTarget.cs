using System;
using System.Threading.Tasks;
using LightLogs.Configs;

namespace LightLogs.Targets
{
    internal class ConsoleTarget : ITarget
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
                ConsoleColor currentColor;

                switch (level)
                {
                    case LogLevel.Trace:
                        currentColor = _consoleTargetConfig.TraceColor;
                        break;

                    case LogLevel.Debug:
                        currentColor = _consoleTargetConfig.DebugColor;
                        break;

                    case LogLevel.Info:
                        currentColor = _consoleTargetConfig.InfoColor;
                        break;

                    case LogLevel.Warning:
                        currentColor = _consoleTargetConfig.WarningColor;
                        break;

                    case LogLevel.Error:
                        currentColor = _consoleTargetConfig.ErrorColor;
                        break;

                    case LogLevel.Fatal:
                        currentColor = _consoleTargetConfig.FatalColor;
                        break;

                    default:
                        currentColor = oldColor;
                        break;
                }

                if (currentColor != oldColor)
                {
                    Console.ForegroundColor = currentColor;
                    Console.Write(log);
                    Console.ForegroundColor = oldColor;
                }
                else
                {
                    Console.Write(log);
                }
            });
        }

        public void Dispose()
        {
        }
    }
}