using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

        public async Task<ResponseMessage> GetMaxWeatherAsync(List<string> cityName)
        {
            foreach (var city in cityName)
            {
                _validator.ValidateCityByName(city);
            }

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var listWeather = await _weatherRepository.GetListWeatherAsync(cityName);

            stopWatch.Stop();
            listWeather.ForEach(x => x.LeadTime = stopWatch.ElapsedMilliseconds);

            foreach (var weather in listWeather)
            {
                if (weather.Main == null)
                {
                    return new ResponseMessage() { IsError = true, Message = $"{cityName} not found.Timer: {weather.LeadTime} ms." };
                }
            }
            return GetMaxWeatherResponseMessage(listWeather);
        }

        private ResponseMessage GetMaxWeatherResponseMessage(List<WeatherResponse> listWeatherResponse)
        {
            var maxWeather = listWeatherResponse.FirstOrDefault(x => x.Main.Temp == listWeatherResponse.Max(t => t.Main.Temp));
            var weatherDTO = new ResponseMessage
            {
                IsError = false,
                Message = $"In {maxWeather.Name}: {maxWeather.Main.Temp} °C now.Timer: {maxWeather.LeadTime} ms. "
            };

            return weatherDTO;
        }

        public async Task<ResponseMessage> GetForecastAsync(string cityName, int days)
        {
            _validator.ValidateForecast(cityName, days);

            var weatherForecast = await _weatherRepository.GetForecastAsync(cityName, days);

            if (weatherForecast.List == null)
            {
                var messageForecast = $"{cityName} not found";
                return new ResponseMessage { Message = messageForecast };
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
            var temp = forecastResponse.List.First();
            var message = $"{forecastResponse.CityName.Name} weather forecast: \n";
            var days = 1;

            forecastResponse.List.ForEach(x => responseMessage.Message = message += string.Join(",", $"Day {days++}" +
             $": {x.Main.Temp}. {GetForecastDescription(x)}\n"));

            return responseMessage;
        }

        private string GetWeatherDescription(WeatherResponse weatherResponse)
        {
            var temperature = weatherResponse.Main.Temp;
            return GetDescription(temperature);
        }

        private string GetForecastDescription(InfoForecast infoForecast)
        {
            var forecastDescription = infoForecast.Main;
            var temperature = forecastDescription.Temp;
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