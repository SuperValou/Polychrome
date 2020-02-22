using System;
using System.Threading.Tasks;

namespace LightLogs.API
{
    public interface ITarget : IDisposable
    {
        Task Write(LogLevel level, char[] log);
    }
}