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
        private IConfiguration _configuration;
        public WeatherRepository(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration; 
        }
        public async Task<WeatherResponse> GetWeatherAsync(string cityName)
        {
            var api = _configuration.Api;
            var key = _configuration.ApiKey;
            var responce = await _httpClient.GetStringAsync($"{api}={cityName}&units=metric&appid={key}");
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responce);
            return weatherResponse;
        }
    }
}
