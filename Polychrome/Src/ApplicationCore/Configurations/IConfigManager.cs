namespace ApplicationCore.Configurations
{
    public interface IConfigManager
    {
         IConfiguration LoadConfig(string configFilePath);
    }
}