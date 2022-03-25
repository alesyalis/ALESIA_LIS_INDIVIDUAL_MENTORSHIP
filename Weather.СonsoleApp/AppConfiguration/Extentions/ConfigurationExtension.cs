using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;

namespace AppConfiguration.Extentions
{
    public static class ConfigurationExtension
    {

        public static IConfigurationRoot PopulateConfigFromAppSetings(this IConfigurationRoot configurationRoot)
        {
            IConfigurationRoot conf = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build();

            return conf;
        }

        public static Config PopulateConfigFromAppConfig(this Config configurationRoot)
        {
            NameValueCollection conf = System.Configuration.ConfigurationManager.AppSettings;
            configurationRoot.ApiKey = conf["apiKey"];
            configurationRoot.UrlBase = conf["urlBase"];
            configurationRoot.UrlWeather = conf["urlWeather"];
            configurationRoot.UrlForecast = conf["urlForecast"];
            configurationRoot.UrlLocationCity = conf["urlLocationCity"];
            configurationRoot.Days = int.Parse(conf["days"]);
            configurationRoot.IsDebug = bool.Parse(conf["isDebug"]);
            configurationRoot.Canceled = int.Parse(conf["canceled"]);

            return configurationRoot;
        }
        public static Config GetWebAPIConfig(this Config configuration)
        {
            var conf = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                .Build();
            configuration.ApiKey = conf["apiKey"];
            configuration.UrlBase = conf["urlBase"];
            configuration.UrlWeather = conf["urlWeather"];
            configuration.UrlForecast = conf["urlForecast"];
            configuration.UrlLocationCity = conf["urlLocationCity"];
            configuration.Days = int.Parse(conf["days"]);
            configuration.IsDebug = bool.Parse(conf["isDebug"]);

            return configuration;
        }
    }
}
