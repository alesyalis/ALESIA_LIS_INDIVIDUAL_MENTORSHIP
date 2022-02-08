using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        public string Url { get; set; }

        public string ApiKey { get; set; }

        public ConfigTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                   .Build();
            ApiKey = config["AppString:0:ApiKey"];
            Url = config["AppString:0:Url"];
        }
    }
   

}
