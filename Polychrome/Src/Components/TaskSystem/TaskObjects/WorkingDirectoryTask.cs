using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TaskSystem.Progresses;

namespace TaskSystem.TaskObjects
{
    public abstract class WorkingDirectoryTask : ITask
    {
        public string WorkingDirectory { get; internal set; }

        public ICollection<string> OutputFiles { get; } = new HashSet<string>();
        
        public abstract Task Execute(IProgressReporter reporter);

        internal void CleanUp()
        {
            foreach (var filePath in Directory.EnumerateFiles(WorkingDirectory, "*.*", SearchOption.AllDirectories))
            {
                if (OutputFiles.Contains(filePath))
                {
                    continue;
                }

                File.Delete(filePath);
            }
        }
    }
}
