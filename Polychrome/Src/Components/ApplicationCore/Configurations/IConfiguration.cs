namespace ApplicationCore.Configurations
{
    public interface IConfiguration
    {
        string AppName { get; }

        string AppVersion { get; }
    }
}