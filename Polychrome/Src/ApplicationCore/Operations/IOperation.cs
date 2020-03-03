using System.Threading.Tasks;

namespace ApplicationCore.Operations
{
    public interface IOperation
    {
        Task Execute();
    }

    public interface IOperationExecution
    {
        void Cancel();
    }

    public interface IProgressReporter
    {

    }
}
