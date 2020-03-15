using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Kernel;
using MetaVid.Configurations;
using TaskSystem.Processes;
using TaskSystem.Progresses;
using TaskSystem.TaskObjects;

namespace MetaVid.Tasks
{
    public class ProbeTask : ITask
    {
        private const string FfProbeCommand =  "-v error -print_format json -show_format -show_streams {0} > {1}"; // {0} is input path, {1} is output path

        private readonly ProbeTaskSetup _setup;
        private readonly string _workingDirectory;

        public ProbeTask(ProbeTaskSetup setup, string workingDirectory)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
        }
        
        public async Task Execute(ILogger reporter)
        {
            // TODO: working dir logic to extract
            if (Directory.Exists(_workingDirectory))
            {
                Directory.Delete(_workingDirectory);
            }

            Directory.CreateDirectory(_workingDirectory);

            reporter.Debug("Listing files...");
            
            string listingOutputFile = Path.Combine(_workingDirectory, "1-listing.txt");
            
            // actual work
            await using (var outputWriter = File.CreateText(listingOutputFile))
            {
                ICollection<string> allowedExtension = new HashSet<string>(_setup.SourceExtensions);
                foreach (var filePath in Directory.EnumerateFiles(_setup.SourceFolder, "*.*", SearchOption.AllDirectories))
                {
                    string fileExt = Path.GetExtension(filePath);
                    if (!allowedExtension.Contains(fileExt))
                    {
                        continue;
                    }

                    outputWriter.WriteLine(filePath);
                }
            }

            reporter.Debug("Listing files ended");

            reporter.Debug("Probing files...");
            string probingOutputFile = Path.Combine(_workingDirectory, "2-probing.txt");
            await using (var outputWriter = File.CreateText(probingOutputFile))
            {
                ILogger ffprobeLogger = reporter.CreateSubLogger("FFProbe");
                using (var reader = File.OpenText(listingOutputFile))
                {
                    while (!reader.EndOfStream)
                    {
                        string inputFilePath = reader.ReadLine();
                        if (string.IsNullOrEmpty(inputFilePath))
                        {
                            continue;
                        }

                        string outputFileName = Path.GetFileNameWithoutExtension(inputFilePath);
                        string outputFilePath = Path.Combine(_workingDirectory, outputFileName + "-raw.json");

                        string args = string.Format(FfProbeCommand, inputFilePath, outputFilePath);

                        var process = new ExeProcess(ffprobeLogger, _setup.FfProbePath, args);
                        await process.Run();

                        outputWriter.WriteLine(inputFilePath);
                        outputWriter.WriteLine(outputFilePath);
                    }
                }
            }
                
            

            throw new NotImplementedException();
        }

    }
}
