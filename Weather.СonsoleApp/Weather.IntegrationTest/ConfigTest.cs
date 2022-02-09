using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        public string UrlBase
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["urlBase"];
            }
        }
        public string UrlWeather
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["urlWeather"];
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

        public string UrlForecast => throw new System.NotImplementedException();

        public string UrlLocationCity => throw new System.NotImplementedException();
    }
}
