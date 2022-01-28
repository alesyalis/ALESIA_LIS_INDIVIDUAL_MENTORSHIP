using System.Threading.Tasks;
using Weather.DataAccess.Repositories.Abstrdact;
using Newtonsoft.Json;
using Weather.DataAccess.Models;
using System.Net.Http;
using AppConfiguration.AppConfig;

namespace Weather.DataAccess.Repositories
{
    public class WeatherRepository : IWeatherRepository

    {
        private readonly HttpClient _httpClient;
        private IConfig _configuration;

        public WeatherRepository(IConfig configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration; 
        }

        public async Task<WeatherResponse> GetWeatherAsync(string cityName)
        {
            var api = _configuration.Url;
            var key = _configuration.ApiKey;
            var responce = await _httpClient.GetAsync($"{api}={cityName}&units=metric&appid={key}");
            var responceBody = await responce.Content.ReadAsStringAsync();  
            var weatherResponсe = JsonConvert.DeserializeObject<WeatherResponse>(responceBody);
            return weatherResponсe;
        }
    }
}
