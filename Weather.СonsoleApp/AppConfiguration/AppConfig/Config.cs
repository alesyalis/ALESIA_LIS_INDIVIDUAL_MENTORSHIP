using System.Configuration;

namespace AppConfiguration.AppConfig
{
    public class Config : IConfig
    {
        public string Url { get { return ConfigurationManager.AppSettings["url"]; } }

        public string ApiKey { get { return ConfigurationManager.AppSettings["apiKey"]; } }
    }
}
