using AppConfiguration.AppConfig;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
        private readonly IConfig _config;
        private CancellationTokenSource _cancellationTokenSource;

        public WeatherService(IWeatherRepository weatherRepository, IValidator validator, IConfig config)
        {
            _weatherRepository = weatherRepository;
            _validator = validator;
            _config = config;
            _cancellationTokenSource = new CancellationTokenSource();   
        }

        public async Task<ResponseMessageDTO> GetWeatherAsync(string cityName)
        {
            _validator.ValidateCityByName(cityName);

            var weather = await _weatherRepository.GetWeatherAsync(cityName, _cancellationTokenSource);

            if (weather.Main == null)
            {
                return new ResponseMessageDTO() { IsError = true, Message = $"{cityName} not found" };
            }

            var description = GetWeatherDescription(weather);

            return GetWeatherResponseMessage(weather, description);
        }

        public async Task<ResponseMessageDTO> GetMaxWeatherAsync(IEnumerable<string> cityName, CancellationTokenSource token)
        {
            token = _cancellationTokenSource;
            _validator.ValidateCityNames(cityName);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var listWeather = await _weatherRepository.GetListWeatherAsync(cityName, token);

            stopWatch.Stop();

            listWeather.ToList().ForEach(x => x.LeadTime = stopWatch.ElapsedMilliseconds);

            var message = new ResponseMessageDTO { };
            var responseMessage = new StringBuilder();

            if (_config.IsDebug == true)
            {
                foreach (var weather in listWeather)
                {
                    if (weather.CountFailedRequests > 0)
                    {
                        responseMessage.Append($"City: {weather.Name}. Error: Invalid city name. Timer: {weather.LeadTime} ms.\n");
                    }
                    else
                    {
                        responseMessage.Append($"City: {weather?.Name}. Temperature: {weather?.Main?.Temp}°C. Timer: {weather?.LeadTime} ms.\n");
                    }
                }
            }

            var maxWeather = CalculateTotalsForMessage(listWeather);

            responseMessage.AppendLine($@"City with the highest temperature {maxWeather?.Main.Temp}°C: {maxWeather?.Name}.
Successful request count: {maxWeather.CountSuccessfullRequests}, failed: {maxWeather.CountFailedRequests}, canceled: {maxWeather.Canceled}.");
            message.Message = responseMessage.ToString();
            return message;
        }

        private WeatherResponse CalculateTotalsForMessage(IEnumerable<WeatherResponse> weathers)
        {
            foreach(var weather in weathers)
            {
                if (weather.Main == null)
                    weather.CountFailedRequests++;
                else if (weather.LeadTime > _config.Canceled)
                    weather.Canceled++;
                else
                    weather.CountSuccessfullRequests++;

            }

            var successfullRequests = weathers.Select(x => x.CountSuccessfullRequests).Sum();
            var failedRequests = weathers.Select(x => x.CountFailedRequests).Sum();
            var canceled = weathers.Select(x => x.Canceled).Sum();

            var maxTemp = weathers?.FirstOrDefault(x => x.Main?.Temp == weathers?.Max(t => t?.Main?.Temp));

            maxTemp.CountFailedRequests = failedRequests;
            maxTemp.CountSuccessfullRequests = successfullRequests;
            maxTemp.Canceled = canceled;    

            return maxTemp;
        }

        public async Task<ResponseMessageDTO> GetForecastAsync(string cityName, int days)
        {
            _validator.ValidateForecast(cityName, days);

            var weatherForecast = await _weatherRepository.GetForecastAsync(cityName, days);

            if (weatherForecast.List == null)
            {
                var messageForecast = $"{cityName} not found";
                return new ResponseMessageDTO { Message = messageForecast };
            }
            var responseMessage = GetForecastMessage(weatherForecast);

            return responseMessage;
        }

        private ResponseMessageDTO GetWeatherResponseMessage(WeatherResponse weatherResponse, string description)
        {
            var weatherDTO = new ResponseMessageDTO
            {
                IsError = false,
                Message = $"In {weatherResponse.Name}: {weatherResponse.Main.Temp} °C now. {description} "
            };

            return weatherDTO;
        }

        private ResponseMessageDTO GetForecastMessage(ForecastResponse forecastResponse)
        {
            var responseMessage = new ResponseMessageDTO { };
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