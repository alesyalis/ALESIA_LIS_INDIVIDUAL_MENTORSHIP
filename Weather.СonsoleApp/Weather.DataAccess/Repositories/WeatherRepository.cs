using System.Threading.Tasks;
using Weather.DataAccess.Repositories.Abstrdact;
using Newtonsoft.Json;
using Weather.DataAccess.Models;
using System.Net.Http;

namespace Weather.DataAccess.Repositories
{
    public class WeatherRepository : IWeatherRepositoty
    {
        static readonly HttpClient _client = new HttpClient();
        public async Task<WeatherResponse> GetWeatherAsync(string cityName, string key)
        {
            var responce = await _client.GetStringAsync($"https://api.openweathermap.org/data/2.5/weather?q={cityName}&units=metric&appid={key}");
            var weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(responce);
            return weatherResponse;
        }
    }
}
