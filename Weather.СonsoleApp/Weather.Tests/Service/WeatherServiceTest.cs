using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

        [SetUp]
        public void Setup()
        {
            _weatherRepositoryMock = new Mock<IWeatherRepository>();
            _validatorMock = new Mock<IValidator>();
            
            _weatherService = new WeatherService(
                _weatherRepositoryMock.Object,
                _validatorMock.Object);
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
        public  void GetWeatherAsync_RepositoryThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var expectedExcetpion = new Exception();
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(() => throw  new Exception());

            // Act
            // Assert
            var result = Assert.ThrowsAsync<Exception>( () => _weatherService.GetWeatherAsync(name));
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

        [TestCase("Minsk", 2, -1, "Dress warmly.", "01.01.001 00:00:00")]
        [TestCase("London",3, 10, "It's fresh.", "01.01.001 00:00:00")]
        public async Task GetАщкусфыеAsync_CorrectWeatherIsReceived_IsErrorFalseAndMessageIsGenerated(string cityName, int days, double temp, string description, DateTime date)
        {
            // Arrange
            var main = new ForecastDescription() { Temp = temp, Description = description };
            var list = new List<InfoForecast> () { };
            var listForecast = new InfoForecast { Main = main };
            list.Add(listForecast);
            var weather = new ForecastResponse()
            {
                CityName = new CityForecast { Name = cityName },
                List = list
            };

            var message = $" {date} In {weather.CityName.Name}: {main.Temp} °C now. {main.Description}\n";

            _weatherRepositoryMock.Setup(x => x.GetForecastAsync(cityName, days)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetForecastAsync(cityName, days);

            // Assert
            _validatorMock.Verify(x => x.ValidateForecast(cityName, days), Times.Once());
            Assert.IsNotNull(result);
            Assert.AreEqual(message, result.Message);
            Assert.IsFalse(result.IsError);
        }
    }
}
