namespace TaskSystem.Progresses
{
    public interface IProgressReporter
    {
        void NotifyError(string errorMessage);

        int BeginStep(string message); // beginning 'message'...

        int BeginStep(string message, int substepCount);

        void EndStep(int stepId); // beginning finished
    }
}