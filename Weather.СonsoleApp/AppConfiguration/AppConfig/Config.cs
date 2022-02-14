using System.Configuration;

namespace AppConfiguration.AppConfig
{
    public class Config : IConfig
    {
        public string UrlBase { get { return ConfigurationManager.AppSettings["urlBase"]; } }

        public string UrlWeather { get { return ConfigurationManager.AppSettings["urlWeather"]; } }

        public string UrlForecast { get { return ConfigurationManager.AppSettings["urlForecast"]; } }

        public string ApiKey { get { return ConfigurationManager.AppSettings["apiKey"]; } }

        public string UrlLocationCity { get { return ConfigurationManager.AppSettings["urlLocationCity"]; } }

        public string Days { get { return ConfigurationManager.AppSettings["days"]; }  }
    }
}
