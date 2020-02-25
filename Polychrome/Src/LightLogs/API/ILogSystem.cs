using System;
using System.Collections.Generic;
using Kernel;

namespace LightLogs.API
{
    public interface ILogSystem : IDisposable
    {
        ILogger Initialize();
        ILogger Initialize(LogLevel minLogLevel);
        ILogger Initialize(string rootLoggerName, LogLevel minLogLevel);
        ILogger Initialize(string rootLoggerName, LogLevel minLogLevel, ICollection<ITarget> targets);
    }
}