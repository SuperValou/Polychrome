using System.Threading.Tasks;
using TaskSystem.TaskObjects;

namespace TaskSystem
{
    public interface ITaskManager
    {
        string WorkingDirectory { get; }

        void Initialize(string workingDirectory);

        Task Run(ITask task);
    }
}
