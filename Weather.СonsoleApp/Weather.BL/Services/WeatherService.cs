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

        public async Task<ResponseMessage> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityByName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);

            if (weather.Main == null)
            {
                return new ResponseMessage() { IsError = true, Message = $"{cityName} not found" };
            }

            var description = GetWeatherDescription(weather);

            return GetWeatherResponseMessage(weather, description);

        }

        public async Task<ResponseMessage> GetForecastAsync(string cityName, int days)
        {
            _validator.ValidateForecast(cityName, days);

            var weatherForecast = await _weatherRepository.GetForecastAsync(cityName, days);

            if (weatherForecast.List == null)
            {
                var messageForecast = $"{cityName} not found";
                var messages = new ResponseMessage { Message = messageForecast };
                return messages;
            }
            var responseMessage = GetForecastMessage(weatherForecast);
            
            return responseMessage;
        }

        private ResponseMessage GetWeatherResponseMessage(WeatherResponse weatherResponse, string description)
        {
            var weatherDTO = new ResponseMessage
            {
                IsError = false,
                Message = $"In {weatherResponse.Name}: {weatherResponse.Main.Temp} °C now. {description} "
            };
            return weatherDTO;
        }

        private ResponseMessage GetForecastMessage(ForecastResponse forecastResponse)
        {
            var responseMessage = new ResponseMessage { };
            foreach (var infoForecast in forecastResponse.List)
            {
                var main = infoForecast.Main;
                var dect = GetForecastDescription(infoForecast);
                var date = infoForecast.Date;

                var weatherDTO = new ResponseMessage
                {
                    Message = $" {date} In {forecastResponse.CityName.Name}: {main.Temp} °C now. {dect}"
                };
                responseMessage.Message += $"{weatherDTO.Message}\n";
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
            if (temperature < 0)
                return "Dress warmly.";
            if (temperature >= 0 && temperature <= 20)
                return "It's fresh.";
            if (temperature > 20 && temperature <= 30)
                return "Good weather!";
            else
                return "It's time to go to the beach";
        }
    }
}
