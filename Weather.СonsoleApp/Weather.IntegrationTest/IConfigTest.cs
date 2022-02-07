using AppConfiguration.AppConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.IntegrationTest
{
    public class IConfigTest : IConfig
    {
        //private string Url { get { return "https://api.openweathermap.org/data/2.5/weather?q"; } }

        //string ApiKey { get { return "8e943ed8b016561c73b8a1920366ef79"; } }
        public string Url =>  "https://api.openweathermap.org/data/2.5/weather?q";

        public string ApiKey => "8e943ed8b016561c73b8a1920366ef79";
    }
}
