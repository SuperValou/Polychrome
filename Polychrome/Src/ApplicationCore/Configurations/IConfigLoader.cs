namespace ApplicationCore.Configurations
{
    public interface IConfigLoader
    {
         IConfiguration LoadConfig(string configFilePath);
    }
}