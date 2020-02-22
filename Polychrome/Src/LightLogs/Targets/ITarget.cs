using System;
using System.Threading.Tasks;

namespace LightLogs.Targets
{
    public interface ITarget : IDisposable
    {
        Task Write(LogLevel level, char[] log);
    }
}