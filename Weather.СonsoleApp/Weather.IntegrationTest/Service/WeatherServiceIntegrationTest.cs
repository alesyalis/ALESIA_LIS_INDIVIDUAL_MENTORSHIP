using AppConfiguration.Interface;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.Exceptions;
using Weather.BL.Services;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;
using Weather.СonsoleApp.Commands;
using Xunit;

namespace Weather.IntegrationTest.Service
{
    public class WeatherServiceIntegrationTest
    {
        private readonly WeatherService _weatherService;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly ConfigTest _configuration;
        private readonly ICommand _commandForecast;
        private readonly ICommand _commandWeather;


        public WeatherServiceIntegrationTest()
        {
            _configuration = new ConfigTest();
            _weatherRepository = new WeatherRepository(_configuration);
            _validator = new Validator(_configuration);
            _weatherService = new WeatherService(_weatherRepository, _validator, _configuration);
            _commandForecast = new GetForecastCommand(_weatherService);
            _commandWeather = new GetWeatherCommand(_weatherService);
        }

        [Fact]
        public async Task GetWeatherAsync_CorrectWeatherReceived_IsErrorFalseAndMessageIsGenerated()
        {
            // Arrange
            var name = "Moscow";
            var regex = @"(\w?)(.*?)\.| (\B\W)(.*?)\.";
            var description1 = "Dress warmly.";
            var description2 = "It's fresh.";
            var description3 = "Good weather!";
            var description4 = "It's time to go to the beach";
            var temp = $"^In {name}: {regex} °C now. ({description1}|{description2}|{description3}|{description4})$";

            //Act
            var response = await _weatherService.GetWeatherAsync(name);

            // Assert
            Assert.False(response.IsError);
            Assert.Matches(temp, response.Message);
        }

        [Fact]
        public async Task GetForecastAsync_CorrectWeatherReceived_IsErrorFalseAndMessageIsGenerated()
        {
            // Arrange
            var name = "Moscow";
            var days = 3;

            var regex = @"(\w?)(.*?)\.| (\B\W)(.*?)\.";

            var description = new string[]
            {
                "Dress warmly.",
                "It's fresh.",
                "Good weather.",
                "It's time to go to the beach."
            };

            var mes = $"{name} weather forecast: \n";
            var temp = "";
            var day = 1;
            for (var d = 1; d <= days; d++)
            {
                temp += string.Join(",", $"Day {day++}" +
                $": {regex}. ({description[0]}|{description[1]}|{description[2]}|{description[3]})\n");
            }
            var message = mes + temp;

            //Act
            var response = await _weatherService.GetForecastAsync(name, days);

            // Assert
            Assert.False(response.IsError);
            Assert.Matches(message, response.Message);
        }

        [Fact]
        public async Task GetWeatherAsync_ReceivedIncorrectWeather_IsErrorTrue()
        {
            // Arrange
            var name = "gdrh";
            var message = $"{name} not found";

            //Act
            var response = await _weatherService.GetWeatherAsync(name);

            // Assert
            Assert.True(response.IsError);
            Assert.Equal(message, response.Message);
        }

        [Fact]
        public void GetWeatherAsync_ValidatorThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var message = "Entering the city is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>(() => _validator.ValidateCityByName(name));
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetForecastAsync_ReceivedIncorrectWeather_IsErrorTrue()
        {
            // Arrange
            var name = "qqqqqq";
            var days = 2;
            var message = $"{name} not found";

            //Act
            var response = await _weatherService.GetForecastAsync(name, days);

            // Assert
            Assert.Equal(message, response.Message);
        }

        [Fact]
        public void GetForecastAsync_ValidatorThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var days = 2;
            var message = "Entering the city is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>(() => _validator.ValidateForecast(name, days));
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public void GetForecastAsync_ValidatorThrowsIsExeptionIfDaysNull_ReceivedError()
        {
            // Arrange
            var name = "test";
            var days = 0;
            var message = "Input number of days is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>(() => _validator.ValidateForecast(name, days));
            Assert.Equal(message, result.Message);
        }

        [Fact]
        public async Task GetMaxWeather_CorrectWeatherReceived_IsErrorFalseAndMessageIsGenerated()
        {
            // Arrange
            var names = new List<string>() { "Cuba", "Minsk" };

            var regex = @"(\w?)(.*?)\.| (\B\W)(.*?)\.";

            var message = $"^City with the highest temperature {regex}";

            //Act
            var response = await _weatherService.GetMaxWeatherAsync(names);

            // Assert
            Assert.False(response.IsError);
            Assert.Matches(message, response.Message);
        }

        [Fact]
        public async Task GetMaxWeather_ReceivedIncorrectWeather_IsErrorTrue()
        {
            // Arrange
            var names = new List<string>() { };
            var message = "Entering the city is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>(() => _validator.ValidateCityNames(names));
            Assert.Equal(message, result.Message);
        }
    }
}
