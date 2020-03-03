using Kernel;
using System;
using System.Threading.Tasks;

namespace ApplicationCore.Operations
{
    public abstract class AbstractOperation : IOperation
    {
        protected readonly ILogger Logger;

        protected AbstractOperation(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract Task Execute();
    }
}
