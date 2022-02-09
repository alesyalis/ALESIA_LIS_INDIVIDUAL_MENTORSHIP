using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        public string Url
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["url"];
            }
        }

        public string ApiKey
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["ApiKey"];
            }
        }
    }
}
