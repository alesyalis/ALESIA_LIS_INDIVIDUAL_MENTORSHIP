using System;
using System.Threading.Tasks;
using Weather.BL.DTOs;
using Weather.BL.Services.Abstract;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;

        public WeatherService(IWeatherRepository weatherRepository, IValidator validator)
        {
            _weatherRepository = weatherRepository;
            _validator = validator; 
        }
        public async Task<WeatherResponseDTO> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityByNameName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);
            
            return MapToWeatherResponseDTO(weather);
        }

        private WeatherResponseDTO MapToWeatherResponseDTO(WeatherResponse weatherResponse)
        {
            if (weatherResponse == null)
            {
                throw new ArgumentNullException(nameof(weatherResponse), "There is no such city");
            }
            var weatherDTO = new WeatherResponseDTO
            {
                Name = weatherResponse.Name,
                Main = new TemperatureInfoDTO
                {
                    Temp = weatherResponse.Main.Temp,
                    Description = DescribeTheWeather(weatherResponse.Main.Temp)
                },
            };

            return weatherDTO;

        }

       private string DescribeTheWeather(double temp)
        {
            if (temp < 0)
                return "Dress warmly.";
            if (temp >= 0 && temp <= 20)
                return "It's fresh.";
            if (temp > 20 && temp <= 30)
                return "Good weather!";
            else 
                return "It's time to go to the beach";
           
        }
    }
}
