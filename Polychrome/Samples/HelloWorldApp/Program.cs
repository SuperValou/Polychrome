using CliApplication;

namespace HelloWorldApp
{
    public class Program
    {
        private const string AppName = "HelloWorldApp";
        private const string Version = "0.1.0";

        static void Main(string[] args)
        {
            var cliApp = new CliApp(AppName, Version);
            cliApp.Initialize(args);
            cliApp.Boot();
        }
    }
}
