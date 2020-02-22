using System;
using System.Threading.Tasks;

namespace SimpleLogs.Targets
{
    public interface ITarget : IDisposable
    {
        Task Write(LogLevel level, char[] log);
    }
}