using Kernel;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskSystem
{
    public abstract class AbstractTask : ITask
    {
        public abstract Task Execute(IProgressReporter reporter);
    }
}
