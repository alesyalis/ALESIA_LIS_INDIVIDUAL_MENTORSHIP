using AppConfiguration.AppConfig;
namespace Weather.IntegrationTest
{
    public class IConfigTest : IConfig
    {
        public string Url =>  "https://api.openweathermap.org/data/2.5/weather?q";

        public string ApiKey => "8e943ed8b016561c73b8a1920366ef79";
    }
}
