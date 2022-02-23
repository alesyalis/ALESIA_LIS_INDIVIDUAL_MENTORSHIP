using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.Exceptions;
using Weather.BL.Services;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.Tests.Service
{
    public class WeatherServiceTest
    {
        private WeatherService _weatherService;
        private Mock<IWeatherRepository> _weatherRepositoryMock;
        private Mock<IValidator> _validatorMock;
        private ConfigTest _configuration;
        private CancellationTokenSource _cancellationTokenSource;   

        [SetUp]
        public void Setup()
        {
            _weatherRepositoryMock = new Mock<IWeatherRepository>();
            _validatorMock = new Mock<IValidator>();
            _configuration = new ConfigTest();

            _weatherService = new WeatherService(
                _weatherRepositoryMock.Object,
                _validatorMock.Object,
                _configuration);
            _cancellationTokenSource = new CancellationTokenSource();   
        }

        [TestCase("Minsk", -1, "Dress warmly.")]
        [TestCase("London", 10, "It's fresh.")]
        [TestCase("Kenya", 30, "Good weather!")]
        [TestCase("Gambia", 35, "It's time to go to the beach")]
        public async Task GetWeatherAsync_CorrectWeatherIsReceived_IsErrorFalseAndMessageIsGenerated(string cityName, double temp, string description)
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = cityName,
                Main = new TemperatureInfo() { Temp = temp, Description = description }
            };
            var message = $"In {weather.Name}: {weather.Main.Temp} °C now. {weather.Main.Description} ";

            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(cityName)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetWeatherAsync(cityName);

            // Assert
            _validatorMock.Verify(x => x.ValidateCityByName(cityName), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
            Assert.IsFalse(result.IsError);
        }


        [TestCase("test")]
        [TestCase("!!!!")]
        public async Task GetWeatherAsync_ReceivedIncorrectWeather_IsErrorTrueAndMessageIsGenerated(string name)
        {
            // Arrange
            var weather = new WeatherResponse() { };
            var message = $"{name} not found";
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(() => weather);

            // Act
            var result = await _weatherService.GetWeatherAsync(name);

            // Assert
            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
            Assert.IsTrue(result.IsError);
        }


        [Test]
        public void GetWeatherAsync_RepositoryThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var expectedExcetpion = new Exception();
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(() => throw new Exception());

            // Act
            // Assert
            var result = Assert.ThrowsAsync<Exception>(() => _weatherService.GetWeatherAsync(name));
            Assert.AreEqual(expectedExcetpion.Message, result.Message);
        }


        [Test]
        public void GetWeatherAsync_ValidatorThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var expectedExcetpion = new ValidationException("Entering the city is required\n");
            _validatorMock.Setup(x => x.ValidateCityByName(name)).Throws(expectedExcetpion);

            // Act
            // Assert
            Exception result = Assert.ThrowsAsync<ValidationException>(() => _weatherService.GetWeatherAsync(name));
            Assert.AreEqual(expectedExcetpion.Message, result.Message);
        }


        [TestCase("London", 3, 10, "It's fresh.")]
        public async Task GetForecastAsync_CorrectWeatherIsReceived_IsErrorFalseAndMessageIsGenerated(string cityName, int days, double temp, string description)
        {
            // Arrange
            var main = new ForecastDescription() { Temp = temp, Description = description };
            var list = new List<InfoForecast>() { };
            var listForecast = new InfoForecast { Main = main };
            list.Add(listForecast);
            var weather = new ForecastResponse()
            {
                CityName = new CityForecast { Name = cityName },
                List = list
            };
            var date = 1;


            var message = $"{weather.CityName.Name} weather forecast: \n";
            weather.List.ForEach(x => message += string.Join(",", $"Day {date++}" +
                $": {x.Main.Temp}. {main.Description}\n"));

            _weatherRepositoryMock.Setup(x => x.GetForecastAsync(cityName, days)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetForecastAsync(cityName, days);

            // Assert
            _validatorMock.Verify(x => x.ValidateForecast(cityName, days), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
            Assert.IsFalse(result.IsError);
        }


        [TestCase("testtt", 2)]
        public async Task GetForecastAsync_ReceivedIncorrectWeather_IsErrorTrueAndMessageIsGenerated(string name, int days)
        {
            // Arrange
            var weather = new ForecastResponse() { };
            var message = $"{name} not found";
            _weatherRepositoryMock.Setup(x => x.GetForecastAsync(name, days)).ReturnsAsync(() => weather);

            // Act
            var result = await _weatherService.GetForecastAsync(name, days);

            // Assert
            _validatorMock.Verify(x => x.ValidateForecast(name, days), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
        }


        [Test]
        public void GetForecastAsync_ValidatorThrowsIsExeptionIfCityNameEmpty_ReceivedError()
        {
            // Arrange
            var name = "";
            var days = 2;
            var expectedExcetpion = new ValidationException("Entering the city is required\n");
            _validatorMock.Setup(x => x.ValidateForecast(name, days)).Throws(expectedExcetpion);

            // Act
            // Assert
            Exception result = Assert.ThrowsAsync<ValidationException>(() => _weatherService.GetForecastAsync(name, days));
            Assert.AreEqual(expectedExcetpion.Message, result.Message);
        }


        [Test]
        public void GetForecastAsync_ValidatorThrowsIsExeptionIfDaysNull_ReceivedError()
        {
            // Arrange
            var name = "Minsk";
            var days = 0;
            var expectedExcetpion = new ValidationException("Input number of days is required\n");
            _validatorMock.Setup(x => x.ValidateForecast(name, days)).Throws(expectedExcetpion);

            // Act
            // Assert
            Exception result = Assert.ThrowsAsync<ValidationException>(() => _weatherService.GetForecastAsync(name, days));
            Assert.AreEqual(expectedExcetpion.Message, result.Message);
        }

        [Test]
        public async Task GetListWeatherAsync_ReceivedСorrectWeather_IsErrorTrueAndMessageIsGenerated()
        {
            // Arrange
            var names = new List<string>() { "Cuba", "Minsk" };

            var maxWeather = new WeatherResponse()
            {
                Name = "Cuba",
                CountFailedRequests = 0,
                CountSuccessfullRequests = 0,
                Main = new TemperatureInfo() { Temp = 25}
            };
            var weather = new List<WeatherResponse>() { maxWeather, new WeatherResponse()
            {
                Name = "Minsk",
                CountFailedRequests = 0,
                CountSuccessfullRequests = 0,
                Main = new TemperatureInfo() { Temp = 5}
            } };
            var success = 2;
            var message = $"City with the highest temperature {maxWeather.Main.Temp}°C: {maxWeather.Name}." +
                  $"\r\nSuccessful request count: {success}, failed: {maxWeather.CountFailedRequests}.\r\n";

            _weatherRepositoryMock.Setup(x => x.GetListWeatherAsync(names, _cancellationTokenSource)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetMaxWeatherAsync(names);

            // Assert
            _validatorMock.Verify(x => x.ValidateCityNames(names), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
        }
    }
}
