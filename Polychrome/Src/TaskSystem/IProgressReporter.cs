namespace TaskSystem
{
    public interface IProgressReporter
    {
        void ReportError(string errorMessage);
    }
}