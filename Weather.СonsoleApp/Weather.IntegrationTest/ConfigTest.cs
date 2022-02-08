using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;
using System;

namespace Weather.IntegrationTest
{
    public class ConfigTest : IConfig
    {
        public string Url { get; set; }

        public string ApiKey { get; set; }

        // public string ApiKey => "8e943ed8b016561c73b8a1920366ef79";

        //public string ApiKey { get { return ConfigurationManager.AppSettings["apiKey"]; } }
        public ConfigTest()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                   .Build();
            ApiKey = config["AppString:0:ApiKey"];
            Url = config["AppString:0:Url"];
        }
    }
   

}
