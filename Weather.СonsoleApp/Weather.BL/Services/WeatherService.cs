using System.Collections.Generic;
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
      
        public async Task<WeatherResponseMessage> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityByName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);
          
            if (weather.Main == null)
            {
                return new WeatherResponseMessage() { IsError = true, Message = $"{cityName} not found" };
            }

            var description = GetWeatherDescription(weather);
            
            return GetWeatherResponseMessage(weather, description);

        }

        public async Task<List<ForecastResponseMessage>> GetForecastAsync(string cityName, int days)
        {
            _validator.ValidateCityByName(cityName);

            var weather = await _weatherRepository.GetForecastAsync(cityName, days);

            if (weather.List == null)
            {
                var message = new ForecastResponseMessage() { IsError = true, Message = $"{cityName} not found" };
                var list = new List<ForecastResponseMessage>();
                list.Add(message);
                return list;
            }

            return GetForecastMessage(weather);
        }

        private WeatherResponseMessage GetWeatherResponseMessage(WeatherResponse weatherResponse, string description)
        {
            var weatherDTO = new WeatherResponseMessage
            {
                IsError = false,
                Message = $"In {weatherResponse.Name}: {weatherResponse.Main.Temp} °C now. {description} "
            };
            return weatherDTO;
        }

        private List<ForecastResponseMessage> GetForecastMessage(ForecastResponse forecastResponse)
        {
            var responseMessage = new List<ForecastResponseMessage> { };

            foreach (var infoForecast in forecastResponse.List)
            {
                var main = infoForecast.Main;
                var dect = GetForecastDescription(infoForecast);
                var date = infoForecast.Dt_txt;

                var weatherDTO = new ForecastResponseMessage
                {
                    Message = $" {date} In {forecastResponse.City.Name}: {main.Temp} °C now. {dect}"
                };
                responseMessage.Add(weatherDTO);
            }
            return responseMessage;
        }

        private string GetWeatherDescription(WeatherResponse weatherResponse)
        {
            var temperature = weatherResponse.Main.Temp;
            var description = weatherResponse.Main.Description;
            return GetDescription(temperature);
        }

        private string GetForecastDescription(InfoForecast infoForecast)
        {
            var forecastDescription = infoForecast.Main;
            var temperature = forecastDescription.Temp;
            var description = forecastDescription.Description;
            return GetDescription(temperature);
            
        }

        private string GetDescription(double temperature)
        {
            var description = "";
            if (temperature < 0)
                return description = "Dress warmly.";
            if (temperature >= 0 && temperature <= 20)
                return description = "It's fresh.";
            if (temperature > 20 && temperature <= 30)
                return description = "Good weather!";
            else
                return description = "It's time to go to the beach";
        }
    }
}
