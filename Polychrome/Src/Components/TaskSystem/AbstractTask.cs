using Kernel;
using System;
using System.Threading.Tasks;

namespace TaskSystem
{
    public abstract class AbstractTask : ITask
    {
        protected readonly ILogger Logger;

        protected AbstractTask(ILogger logger)
        {
            
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public abstract Task Execute(IProgressReporter reporter);
    }
}
