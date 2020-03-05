using System.Threading.Tasks;

namespace ApplicationCore.Configurations
{
    public interface IConfigLoader
    {
         Task<IConfiguration> LoadConfig(string configFilePath);
    }
}