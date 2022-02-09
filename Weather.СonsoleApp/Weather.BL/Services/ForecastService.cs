using System.Collections.Generic;
using System.Linq;
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

            var description = GetForecastDescription(weather);

            return MapToWeatherResponseDTO(weather, description);

        }
        private List<ForecastResponseMessage> MapToWeatherResponseDTO(ForecastResponse forecastResponse, string description)
        {
            var responseMessage = new List<ForecastResponseMessage> { };

            foreach (var infoForecast in forecastResponse.List)
            {
                var main = new InfoForecast
                { Main = infoForecast.Main };

                var weatherDTO = new ForecastResponseMessage
                {
                    Message = $"In {forecastResponse.City.Name}: {main.Main.Temp} °C now. {description}"
                };
                responseMessage.Add(weatherDTO);
            }
            return responseMessage;
        }
        private string GetForecastDescription(ForecastResponse weatherResponse)
        {
            var temp = weatherResponse.List;

            var temperature = temp.FirstOrDefault().Main.Temp;
            var description = temp.FirstOrDefault().Main.Description;
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
