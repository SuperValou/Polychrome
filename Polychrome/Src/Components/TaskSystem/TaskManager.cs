using System;
using System.IO;
using System.Threading.Tasks;
using Kernel;
using TaskSystem.TaskObjects;

namespace TaskSystem
{
    public class TaskManager : ITaskManager
    {
        private readonly ILogger _logger;

        private string _workingDirectory;

        public TaskManager(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize(string workingDirectory)
        {
            _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            if (!Directory.Exists(_workingDirectory))
            {
                Directory.CreateDirectory(_workingDirectory);
            }
        }

        public Task Run(ITask task)
        {
            throw new NotImplementedException();
        }
    }
}