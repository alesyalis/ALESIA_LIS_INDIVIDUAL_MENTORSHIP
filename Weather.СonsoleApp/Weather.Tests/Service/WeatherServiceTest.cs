using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
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

        [Test]
        public async Task GetWeatherAsync_IfDescriptionDressWarmly_Sucess()
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = "Minsk",
                Main = new TemperatureInfo() { Temp = -1, Description = "Dress warmly." }
            };
            var name = "Minsk";
            var message = $"В {weather.Name}: {weather.Main.Temp} °C {weather.Main.Description} ";
            
            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Never());
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(weather);
            
            // Act
            var result = await _weatherService.GetWeatherAsync(name);
            
            // Assert
            Assert.AreEqual(message, result.Message);
        }

        [Test]
        public async Task GetWeatherAsync_IfDescriptionGoodWeather_Sucess()
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = "London",
                Main = new TemperatureInfo() { Temp = 30, Description = "Good weather!" }
            };
            var name = "London";
            var message = $"В {weather.Name}: {weather.Main.Temp} °C {weather.Main.Description} ";
            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Never());
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetWeatherAsync(name);

            // Assert
           Assert.AreEqual(message, result.Message);
        }

        [Test]
        public async Task GetWeatherAsync_IfTemperature10_Sucess()
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = "Minsk",
                Main = new TemperatureInfo() { Temp = 10, Description = "It's fresh." }
            };
            var name = "Minsk";
            var message = $"В {weather.Name}: {weather.Main.Temp} °C {weather.Main.Description} ";

            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Never());
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetWeatherAsync(name);

            // Assert
            Assert.AreEqual(message, result.Message);
        }

        [Test]
        public async Task GetWeatherAsync_IfTemperatureMore30_Sucess()
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = "Minsk",
                Main = new TemperatureInfo() { Temp = 35, Description = "It's time to go to the beach" }
            };
            var name = "Minsk";
            var message = $"В {weather.Name}: {weather.Main.Temp} °C {weather.Main.Description} ";

            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Never());
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(weather);

            // Act
            var result = await _weatherService.GetWeatherAsync(name);

            // Assert
            Assert.AreEqual(message, result.Message);
        }

        [Test]
        public async Task GetWeatherAsync_IfUnCorrectCityName_ShouldReturnNothing()
        {
            // Arrange
            var weather = new WeatherResponse()
            {
                Name = "",
            };
            var name = "fuf";
            var message = $"{name} not found";

            // Act
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(() => weather);
            var result = await _weatherService.GetWeatherAsync(name);

            // Assert
             //Assert.ThrowsAsync<ArgumentNullException>(async () => await _weatherService.GetWeatherAsync(name));
            Assert.AreEqual(message, result.Message);
        }
    }
}
