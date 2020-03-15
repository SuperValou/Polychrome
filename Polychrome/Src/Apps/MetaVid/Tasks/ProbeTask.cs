using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MetaVid.Configurations;
using TaskSystem.Progresses;
using TaskSystem.TaskObjects;

namespace MetaVid.Tasks
{
    public class ProbeTask : WorkingDirectoryTask
    {
        private const string FfProbeCommand =  "-v error -print_format json -show_format -show_streams {0} > {1}"; // {0} is input path, {1} is output path

        private readonly ProbeTaskSetup _setup;
        
        public ICollection<string> OutputFiles { get; } = new HashSet<string>();

        public ProbeTask(ProbeTaskSetup setup)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
        }
        
        public override Task Execute(IProgressReporter reporter)
        {
            int stepId = reporter.BeginStep("Listing files");
            
            ICollection<string> allowedExtension = new HashSet<string>(_setup.SourceExtensions.Where(ext => !string.IsNullOrEmpty(ext)));
            ICollection<string> filePaths = new List<string>();
            foreach (var filePath in Directory.EnumerateFiles(_setup.SourceFolder, "*.*", SearchOption.AllDirectories))
            {
                string fileExt = Path.GetExtension(filePath);
                if (!allowedExtension.Contains(fileExt))
                {
                    continue;
                }

                filePaths.Add(filePath);
            }

            reporter.EndStep(stepId);

            stepId = reporter.BeginStep("Probing", filePaths.Count);
            
            foreach (var filePath in filePaths)
            {
                string outputPath = GetOutputPath();
                //var startInfo = new ProcessStartInfo()
                //{
                //    FileName = _setup.FfProbePath,
                //    Arguments = 
                //}
            }

            reporter.EndStep(stepId);

            throw new NotImplementedException();
        }

        private string GetOutputPath()
        {
            throw new NotImplementedException();
        }
    }
}
