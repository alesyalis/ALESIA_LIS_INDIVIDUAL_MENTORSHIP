using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;
using Microsoft.Extensions.Configuration;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        private readonly IConfigurationRoot _configuration;

        public string UrlBase { get; set; }

        public string UrlWeather { get; set; }

        public string ApiKey { get; set; }

        public string UrlForecast { get; set; }

        public string UrlLocationCity { get; set; }

        public int Days { get; set; }

        public bool IsDebug { get; set; }

        public ConfigTest()
        {
            var conf = _configuration.PopulateConfigFromAppSetings();

            ApiKey = conf["ApiKey"];
            UrlBase = conf["urlBase"];
            UrlWeather = conf["urlWeather"];
            UrlForecast = conf["urlForecast"];
            UrlLocationCity = conf["urlLocationCity"];
            Days = int.Parse(conf["days"]);
            IsDebug = bool.Parse(conf["isDebug"]);
        }
    }
}
