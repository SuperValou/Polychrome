using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using ApplicationCore.Tasks;
using Kernel;
using MetaVid.Configurations;
using MetaVid.Tasks.ProbeDTO;

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
            _logger.Info($"Probing {_setup.SourceFolder}...");

            ILogger ffprobeLogger = _logger.CreateSubLogger("FFProbe");
            int probedFileCount = 0;
            foreach (var fileToProbe in Directory.EnumerateFiles(_setup.SourceFolder, Pattern, SearchOption.AllDirectories))
            {
                string outputFileName = Path.GetFileNameWithoutExtension(fileToProbe);
                string ffprobeOutputFilePath = Path.Combine(_workingDirectory, outputFileName + "-raw.json");

                string args = string.Format(FfProbeCommand, fileToProbe, ffprobeOutputFilePath);

                bool success;
                using (var process = new ExeProcess(ffprobeLogger, _setup.FfProbePath, args))
                {
                    success = await process.Run();
                }

                if (!success)
                {
                    _logger.Debug($"Skipping '{fileToProbe}' because of error.");
                    continue;
                }

                await using (var reader = File.OpenRead(ffprobeOutputFilePath))
                {
                    var probedData = await JsonSerializer.DeserializeAsync<ProbedData>(reader);

                    string outputMetafilePath = Path.Combine(_workingDirectory, $"{outputFileName}.json");

                    await using (var writer = File.Create(outputMetafilePath))
                    {
                        var options = new JsonSerializerOptions() {WriteIndented = true};
                        await JsonSerializer.SerializeAsync(writer, probedData.Format, options);
                    }
                }

                _logger.Debug($"Probed '{outputFileName}'");
                probedFileCount++;
            }

            _logger.Info($"Probed {probedFileCount} files.");
        }
    }
}
