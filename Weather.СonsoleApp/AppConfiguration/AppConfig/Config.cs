using AppConfiguration.Extentions;
using System.Collections.Specialized;

namespace AppConfiguration.AppConfig
{
    public class Config : IConfig
    {
        private readonly NameValueCollection _configuration;
        public string UrlBase { get; }

        public string UrlWeather { get; }

        public string UrlForecast { get; }

        public string ApiKey { get; }

        public string UrlLocationCity { get; }

        public int Days { get; }
        public Config()
        {
            var conf = _configuration.GetConfig();

            ApiKey = conf["apiKey"];
            UrlBase = conf["urlBase"];
            UrlWeather = conf["urlWeather"];
            UrlForecast = conf["urlForecast"];
            UrlLocationCity = conf["urlLocationCity"];
            Days = int.Parse(conf["days"]);
        }
    }
}
