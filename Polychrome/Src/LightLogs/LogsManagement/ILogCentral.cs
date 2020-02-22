namespace SimpleLogs.LogsManagement
{
    internal interface ILogCentral
    {
        void AddLogEvent(LogEvent logEvent);
    }
}