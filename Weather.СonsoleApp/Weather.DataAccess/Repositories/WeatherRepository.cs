using System.Threading.Tasks;
using Weather.DataAccess.Repositories.Abstrdact;
using Newtonsoft.Json;
using Weather.DataAccess.Models;
using System.Net.Http;
using AppConfiguration.AppConfig;
using System.Collections.Generic;
using System.Linq;

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
            var urlBase = _configuration.UrlBase;
            var urlWeather = _configuration.UrlWeather;
            var key = _configuration.ApiKey;
            var responce = await _httpClient.GetAsync($"{urlBase}{urlWeather}={cityName}&units=metric&appid={key}");
            var responceBody = await responce.Content.ReadAsStringAsync();
            var weatherResponсe = JsonConvert.DeserializeObject<WeatherResponse>(responceBody);
            return weatherResponсe;
        }
        public async Task<ForecastResponse> GetForecastAsync(string cityName, int days)
        {
            var locations = await GetLocationAsync(cityName);
            var locationLatitudeat = locations.FirstOrDefault()?.LocationLatitude;
            var locationLongitudeon = locations.FirstOrDefault()?.LocationLongitude;
            var urlBase = _configuration.UrlBase;
            var urlForecast = _configuration.UrlForecast;
            var key = _configuration.ApiKey;

            var response = await _httpClient.GetAsync($"{urlBase}{urlForecast}lat={locationLatitudeat}&lon={locationLongitudeon}&cnt={days}&units=metric&appid={key}");
            var responceBody = await response.Content.ReadAsStringAsync();
            var weatherResponсe = JsonConvert.DeserializeObject<ForecastResponse>(responceBody);

            return weatherResponсe;
        }
        public async Task<IEnumerable<LocationCity>> GetLocationAsync(string cityName)
        {
            var key = _configuration.ApiKey;
            var url = _configuration.UrlLocationCity;
            var response = await _httpClient.GetAsync($"{url}{cityName}&appid={key}");
            var responceBody = await response.Content.ReadAsStringAsync();
            var locationResponсe = JsonConvert.DeserializeObject<IEnumerable<LocationCity>>(responceBody);

            return locationResponсe;
        }

        public async Task<IEnumerable<WeatherResponse>> GetListWeatherAsync(IEnumerable<string> cityName)
        {
            var tasks = cityName.Select(async city =>
            {
                var listWeatherResponse = await GetWeatherAsync(city);
                return listWeatherResponse;
            }).ToList();

            var results = await Task.WhenAll(tasks);
            return results?.ToList();
        }
    }
}