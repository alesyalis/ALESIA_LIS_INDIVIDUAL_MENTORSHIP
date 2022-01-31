using Moq;
using NUnit.Framework;
using System.Threading.Tasks;
using Weather.BL.DTOs;
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
        public async Task GetWeatherAsync_IfCorrectCityName_Sucess()
        {
            // Arrange
            var weater = new WeatherResponse()
            {
                Name = "Minsk",
                Main = new TemperatureInfo() { Temp = 5, }

            };
            // Act
            var name = "Minsk";
            _validatorMock.Verify(x => x.ValidateCityByName(name), Times.Never());
            _weatherRepositoryMock.Setup(x => x.GetWeatherAsync(name)).ReturnsAsync(weater);

            var result = await _weatherService.GetWeatherAsync(name);
            var dto = new WeatherResponseDTO()
            {
                Name = "Minsk",
                Main = new TemperatureInfoDTO() { Temp = 5, }
            };

            // Assert
            Assert.AreEqual(dto.Main.Temp, result.Main.Temp);
        }
    }
}
