using System;
using CliApplication;

namespace HelloWorldApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            CliApp cliApp = new TmdbCrawlerApp();
            cliApp.Initialize(args);
            cliApp.Run();
            cliApp.Dispose();
        }
    }
}
