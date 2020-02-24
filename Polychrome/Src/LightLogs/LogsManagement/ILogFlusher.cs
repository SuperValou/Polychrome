using System;

namespace LightLogs.LogsManagement
{
    internal interface ILogFlusher : IDisposable
    {
        void AddLogEvent(LogEvent logEvent);
        void Initialize();
    }
}