using System.Threading.Tasks;
using TaskSystem.Progresses;

namespace TaskSystem.TaskObjects
{
    public abstract class AbstractTask : ITask
    {
        public abstract Task Execute(IProgressReporter reporter);
    }
}
