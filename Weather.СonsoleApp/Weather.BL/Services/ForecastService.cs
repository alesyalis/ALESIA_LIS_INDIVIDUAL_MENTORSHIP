using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.BL.DTOs;
using Weather.BL.Services.Abstract;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;

        public ForecastService(IWeatherRepository weatherRepository, IValidator validator)
        {
            _weatherRepository = weatherRepository;
            _validator = validator;
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

            return MapToWeatherResponseDTO(weather);
        }

        private List<ForecastResponseMessage> MapToWeatherResponseDTO(ForecastResponse forecastResponse)
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
        private string GetForecastDescription(InfoForecast infoForecast)
        {
            var forecastDescription = infoForecast.Main;
            var temperature = forecastDescription.Temp;
            var description = forecastDescription.Description;

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
