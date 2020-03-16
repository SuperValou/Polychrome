using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using MetaVid.Configurations;

namespace MetaVid.Tasks
{
    public class ProbeTask : ITask
    {
        private const string Pattern = "*.mp4";
        private const string FfProbeCommand =  "-v error -print_format json -show_format -show_streams \"{0}\" > \"{1}\""; // {0} is input path, {1} is output path

        private readonly ProbeTaskSetup _setup;
        private readonly ILogger _logger;
        private readonly string _workingDirectory;

        public ProbeTask(ProbeTaskSetup setup, ILogger logger, string workingDirectory)
        {
            _setup = setup ?? throw new ArgumentNullException(nameof(setup));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _workingDirectory = workingDirectory ?? throw new ArgumentNullException(nameof(workingDirectory));
        }
        
        public async Task Execute()
        {
            ILogger ffprobeLogger = _logger.CreateSubLogger("FFProbe");
            foreach (var fileToProbe in Directory.EnumerateFiles(_setup.SourceFolder, Pattern, SearchOption.AllDirectories))
            {
                string outputFileName = Path.GetFileNameWithoutExtension(fileToProbe);
                string outputFilePath = Path.Combine(_workingDirectory, outputFileName + "-raw.json");

                string args = string.Format(FfProbeCommand, fileToProbe, outputFilePath);

                bool success;
                using (var process = new ExeProcess(ffprobeLogger, _setup.FfProbePath, args))
                {
                    success = await process.Run();
                }

                if (success)
                {
                    _logger.Debug($"Generated '{outputFilePath}'");
                }
            }
        }
    }
}
