using AppConfiguration.AppConfig;
using Moq;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Weather.BL.Services;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories;
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
            var message = $"In {weather.Name}: {weather.Main.Temp} °C {weather.Main.Description} ";

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

        [TestCase("")]
        public async Task GetWeatherAsync_ReceivedRepositoryError_ReceivedError(string name)
        {
            // Arrange
            var expectedExcetpion = new Exception();
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(() => throw  new Exception());

            // Act

            // Assert
            var result = Assert.ThrowsAsync<Exception>( () => _weatherService.GetWeatherAsync(name));
            Assert.AreEqual(expectedExcetpion.Message, result.Message);  
        }
    }
}
