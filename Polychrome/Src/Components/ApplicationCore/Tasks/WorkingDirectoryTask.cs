using System;
using System.IO;
using System.Threading.Tasks;
using Kernel;
using Kernel.Exceptions;

namespace ApplicationCore.Tasks
{
    public abstract class WorkingDirectoryTask : ITask
    {
        private string _workingDirectory;

        protected string WorkingDirectory
        {
            get
            {
                if (_workingDirectory == null)
                {
                    throw new NotInitializedException(this.GetType().Name);
                }

                return _workingDirectory;
            }
        }

        protected ILogger Logger { get; }

        protected WorkingDirectoryTask(ILogger logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task Initialize(string workingDirectory)
        {
            return Task.Run(() =>
            {
                _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));

                if (Directory.Exists(_workingDirectory))
                {
                    Directory.Delete(workingDirectory, recursive: true);
                }

                Directory.CreateDirectory(_workingDirectory);
            });
        }

        public abstract Task Execute();
    }
}