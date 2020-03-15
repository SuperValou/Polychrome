using System;
using Kernel;

namespace TaskSystem
{
    public interface ITaskManager
    {
        void Initialize(string workingDirectory);
    }

    public class TaskManager : ITaskManager
    {
        private ILogger _logger;

        public TaskManager(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize(string workingDirectory)
        {
            throw new System.NotImplementedException();
        }
    }
}
