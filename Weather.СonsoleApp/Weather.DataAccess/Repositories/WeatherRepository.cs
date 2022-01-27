using System.Threading.Tasks;
using Weather.DataAccess.Repositories.Abstrdact;
using Newtonsoft.Json;
using Weather.DataAccess.Models;
using System.Net.Http;

namespace Weather.DataAccess.Repositories
{
    public class WeatherRepository : IWeatherRepository

    {
        private readonly HttpClient _httpClient;
        public WeatherRepository()
        {
            _httpClient = new HttpClient();
        }
        public async Task<WeatherResponse> GetWeatherAsync(string cityName)
        {
            var responce = await _httpClient.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid=8e943ed8b016561c73b8a1920366ef79");
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responce);
            return weatherResponse;
        }
    }
}
