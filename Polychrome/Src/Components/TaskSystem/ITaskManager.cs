using System.Threading.Tasks;
using TaskSystem.TaskObjects;

namespace TaskSystem
{
    public interface ITaskManager
    {
        void Initialize(string workingDirectory);

        Task Run(ITask task);
    }
}
