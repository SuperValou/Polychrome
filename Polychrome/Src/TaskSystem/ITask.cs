using System.Threading.Tasks;

namespace TaskSystem
{
    public interface ITask
    {
        Task Execute(IProgressReporter reporter);
    }

    public interface ITaskExecution
    {
        void Cancel();
    }

    public interface IProgressReporter
    {

    }
}
