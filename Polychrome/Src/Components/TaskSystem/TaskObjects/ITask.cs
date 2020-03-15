using System.Threading.Tasks;
using TaskSystem.Progresses;

namespace TaskSystem.TaskObjects
{
    public interface ITask
    {
        Task Execute(IProgressReporter reporter);
    }
}
