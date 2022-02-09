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
            
            return MapToWeatherResponseDTO(weather, description);

        }
        private WeatherResponseMessage MapToWeatherResponseDTO(WeatherResponse weatherResponse, string description)
        {
            var weatherDTO = new WeatherResponseMessage
            {
                IsError = false,
                Message = $"In {weatherResponse.Name}: {weatherResponse.Main.Temp} °C now. {description} "
            };
            return weatherDTO;
        }

        private string GetWeatherDescription(WeatherResponse weatherResponse)
        {
            var temperature = weatherResponse.Main.Temp;
            var description = weatherResponse.Main.Description; 
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
