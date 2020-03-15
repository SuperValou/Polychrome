using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Kernel;

namespace ApplicationCore.Tasks
{
    public class ExeProcess
    {
        private readonly ILogger _logger;
        private readonly string _exePath;
        private readonly string _args;

        private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(0);

        public ExeProcess(ILogger logger, string exePath, string args)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _exePath = exePath ?? throw new ArgumentNullException(nameof(exePath));
            _args = args ?? throw new ArgumentNullException(nameof(args));
        }

        public async Task Run()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo()
            {
                FileName = "cmd",
                Verb = "runas",
                Arguments = $"/C \"\"{_exePath}\" {_args}\"",
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,                
            };

            _logger.Debug($"Running: {startInfo.FileName} {startInfo.Arguments}");

            var process = new Process() { StartInfo = startInfo };
            process.EnableRaisingEvents = true;
            process.OutputDataReceived += LogOutput;
            process.ErrorDataReceived += LogError;
            process.Exited += EndAwait;

            try
            {
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await _semaphore.WaitAsync();
                _logger.Debug($"Exit code: {process.ExitCode}");
            }
            finally
            {
                process.OutputDataReceived -= LogOutput;
                process.ErrorDataReceived -= LogError;
                process.Exited -= EndAwait;
                process.Dispose();
            }
        }

        private void EndAwait(object sender, EventArgs e)
        {
            _semaphore.Release();
        }


        private void LogOutput(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            _logger.Debug($"STDOUT: {e.Data}");
        }

        private void LogError(object sender, DataReceivedEventArgs e)
        {
            if (e.Data == null)
            {
                return;
            }

            _logger.Error($"STDERR: {e.Data}");
        }
    }
}