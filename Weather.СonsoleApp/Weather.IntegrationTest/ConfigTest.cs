using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;
using Microsoft.Extensions.Configuration;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        private readonly IConfigurationRoot _configuration;

        public string UrlBase { get; }

        public string UrlWeather { get; }

        public string ApiKey { get; }

        public string UrlForecast { get; }

        public string UrlLocationCity { get; }

        public int Days { get; }

        public ConfigTest()
        {
            var conf = _configuration.GetConfigTest();

            ApiKey = conf["ApiKey"];
            UrlBase = conf["urlBase"];
            UrlWeather = conf["urlWeather"];
            UrlForecast = conf["urlForecast"];
            UrlLocationCity = conf["urlLocationCity"];
            Days = int.Parse(conf["days"]);
        }
    }
}
