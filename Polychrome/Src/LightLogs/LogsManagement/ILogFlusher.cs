namespace LightLogs.LogsManagement
{
    internal interface ILogFlusher
    {
        void AddLogEvent(LogEvent logEvent);
    }
}