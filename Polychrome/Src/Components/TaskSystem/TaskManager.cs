using System;
using System.IO;
using System.Threading.Tasks;
using Kernel;
using TaskSystem.Progresses;
using TaskSystem.TaskObjects;

namespace TaskSystem
{
    public class TaskManager : ITaskManager
    {
        private readonly ILogger _logger;

        public string WorkingDirectory { get; private set; }

        public TaskManager(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Initialize(string workingDirectory)
        {
            WorkingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
            if (!Directory.Exists(WorkingDirectory))
            {
                Directory.CreateDirectory(WorkingDirectory);
            }
        }

        public async Task Run(ITask task)
        {
            var taskLogger = _logger.CreateSubLogger(task.GetType().Name);
            await task.Execute(taskLogger);
        }
    }

    public class ProgressReporter : IProgressReporter
    {
        public ProgressReporter(ILogger logger)
        {
            
        }

        public int BeginStep(string message)
        {
            return 0;
        }

        public int BeginStep(string message, int substepCount)
        {
            return 0;
        }

        public void EndStep(int stepId)
        {
            
        }

        public void Debug(string message)
        {
            
        }

        public void Notify(string message)
        {
            
        }

        public void ReportWarning(string message)
        {
            
        }

        public void ReportError(string message)
        {
            
        }

        public void ReportError(string message, Exception exception)
        {
            
        }
    }
}