﻿using AppConfiguration.AppConfig;
using Microsoft.Extensions.Configuration;

namespace Weather.Tests
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

        public string UrlForecast
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["urlForecast"];
            }
        }

        public string UrlLocationCity
        {
            get
            {
                return new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["urlLocationCity"];
            }
        }
        public int Days
        {
            get 
            { 
                return  int.Parse(new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                                 .Build()["days"]); 
            }
        }
    }
}
