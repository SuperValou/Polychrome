using System.Threading.Tasks;

namespace TaskSystem
{
    public interface ITask
    {
        Task Execute(IProgressReporter reporter);
    }
}
