using System.Threading.Tasks;
using CliApplication;

namespace SwapFusion
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            CliApp cliApp = new SwapFusionApp();
            await cliApp.Initialize(args);
            int exitCode = await cliApp.Run();
            cliApp.Dispose();

            return exitCode;
        }
    }
}
