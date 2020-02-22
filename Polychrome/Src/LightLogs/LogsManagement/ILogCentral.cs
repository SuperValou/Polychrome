namespace LightLogs.LogsManagement
{
    internal interface ILogCentral
    {
        void AddLogEvent(LogEvent logEvent);
    }
}