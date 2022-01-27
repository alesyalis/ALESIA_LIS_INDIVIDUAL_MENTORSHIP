using System.Threading.Tasks;
using Weather.BL.DTOs;
using Weather.BL.Services.Abstract;
using Weather.BL.Validators;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Services
{
    public class WeatherServicese : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private WeatherValidator _weatherValidator;
        public WeatherServicese(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }
        public async Task<WeatherResponseDTO> GetWeatherAsync(string cityName)
        {
            _weatherValidator = new WeatherValidator();

            var weather = await _weatherRepository.GetWeatherAsync(cityName);
            _weatherValidator.ValidateCityName(weather);
            return MappWeather(weather);
        }
        private WeatherResponseDTO MappWeather(WeatherResponse weatherResponse)
        {
            var weatherDTO = new WeatherResponseDTO 
            { 
                Name = weatherResponse.Name,
                Main = new TemperatureInfoDTO 
                { 
                    Temp = weatherResponse.Main.Temp,
                    Description = WeatherDescription(weatherResponse.Main.Temp)
                },
            };

            return weatherDTO;  
        }
       private string WeatherDescription(double temp)
        {
            if (temp < 0)
                return "Dress warmly.";
            if (temp >= 0 && temp <= 20)
                return "It's fresh.";
            if (temp > 20 && temp <= 30)
                return "Good weather!";
            if (temp > 30)
                return "It's time to go to the beach";
            else
                return "Nothing";
        }
    }
}
