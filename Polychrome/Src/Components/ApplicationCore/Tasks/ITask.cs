using System.Threading.Tasks;
using Kernel;

namespace ApplicationCore.Tasks
{
    public interface ITask
    {
        Task Execute();
    }
}
