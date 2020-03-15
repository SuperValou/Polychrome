using System.Threading.Tasks;
using Kernel;
using TaskSystem.Progresses;

namespace TaskSystem.TaskObjects
{
    public interface ITask
    {
        Task Execute(ILogger reporter);
    }
}
