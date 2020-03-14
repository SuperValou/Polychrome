using System;
using System.Threading.Tasks;
using CliApplication;

namespace HelloWorldApp
{
    public class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                CliApp cliApp = new TmdbCrawlerApp();
                await cliApp.Initialize(args);
                int exitCode = await cliApp.Run();
                cliApp.Dispose();

                return exitCode;
            }
            catch (Exception e)
            {
#if DEBUG                
                throw new Exception("CRASH", e);
#else
                return ExitCode.Error;
#endif
            }
        }
    }
}
