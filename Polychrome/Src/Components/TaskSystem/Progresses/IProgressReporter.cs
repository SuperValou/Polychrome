namespace TaskSystem.Progresses
{
    public interface IProgressReporter
    {
        int BeginStep(string message); // beginning 'message'...

        int BeginStep(string message, int substepCount);

        void EndStep(int stepId); // beginning finished
    }
}