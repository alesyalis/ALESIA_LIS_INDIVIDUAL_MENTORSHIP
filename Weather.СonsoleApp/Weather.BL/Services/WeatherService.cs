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
      
        public async Task<WeatherResponseNew> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityByName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName);
          
            if (weather.Main == null)
            {
                return new WeatherResponseNew() { IsError = true, Message = $"{cityName} not found" };
            }
            var description = GetWeatherDescription(weather);
            
            return MapToWeatherResponseDTO(weather, description);

        }
        private WeatherResponseNew MapToWeatherResponseDTO(WeatherResponse weatherResponse, string description)
        {
            var weatherDTO = new WeatherResponseNew
            {
                IsError = false,
                Message = $"В {weatherResponse.Name}: {weatherResponse.Main.Temp} °C {description} "
            };
            return weatherDTO;
        }

        private string GetWeatherDescription(WeatherResponse weatherResponse)
        {
            if (weatherResponse.Main.Temp < 0)
                return weatherResponse.Main.Description = "Dress warmly.";
            if (weatherResponse.Main.Temp >= 0 && weatherResponse.Main.Temp <= 20)
                return weatherResponse.Main.Description = "It's fresh.";
            if (weatherResponse.Main.Temp > 20 && weatherResponse.Main.Temp <= 30)
                return weatherResponse.Main.Description = "Good weather!";
            else 
                return weatherResponse.Main.Description = "It's time to go to the beach";
        }
    }
}
