using System.Configuration;

namespace AppConfiguration.AppConfig
{
    public class Config : IConfig
    {
        public string Api { get { return ConfigurationManager.AppSettings["url"]; } }

        public string ApiKey { get { return ConfigurationManager.AppSettings["apiKey"]; } }
    }
}
