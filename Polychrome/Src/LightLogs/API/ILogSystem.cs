using System;
using Kernel;

namespace LightLogs.API
{
    public interface ILogSystem : IDisposable
    {
        ILogger Initialize();
        ILogger Initialize(LogLevel minLogLevel);
        ILogger Initialize(string rootLoggerName, LogLevel minLogLevel);
    }
}