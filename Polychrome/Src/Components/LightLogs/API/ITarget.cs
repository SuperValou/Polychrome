using System;
using System.Threading.Tasks;

namespace LightLogs.API
{
    public interface ITarget : IDisposable
    {
        LogLevel MinLogLevel { get; }

        Task Write(LogLevel level, char[] log);
    }
}