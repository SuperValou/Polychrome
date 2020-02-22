namespace HelloWorldApp
{
    internal class CliApp
    {
        private readonly string _appName;
        private readonly string _version;

        public CliApp(string appName, string version)
        {
            _appName = appName;
            _version = version;
        }
    }
}