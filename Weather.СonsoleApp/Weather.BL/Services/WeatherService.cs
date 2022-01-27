using System;
using System.Threading.Tasks;
using Weather.BL.DTOs;
using Weather.BL.Services.Abstract;
using Weather.BL.Validators;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories;

namespace Weather.BL.Services
{
    public class WeatherServicese : IWeatherService
    {
        private WeatherRepository _weatherRepositoty;
        private WeatherValidator _weatherValidator;
        public async Task<WeatherResponseDTO> GetWeatherAsync(string cityName)
        {
            _weatherValidator = new WeatherValidator(); 
            

            if (_weatherValidator.IsValidCityName(cityName))
            {
                _weatherRepositoty = new WeatherRepository();

                var weather = await _weatherRepositoty.GetWeatherAsync(cityName);
                return MappWeather(weather);
            }
            else
            {
               return new WeatherResponseDTO{
                   ErrorMessage = "Entering the city is required"
               };
            }
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
