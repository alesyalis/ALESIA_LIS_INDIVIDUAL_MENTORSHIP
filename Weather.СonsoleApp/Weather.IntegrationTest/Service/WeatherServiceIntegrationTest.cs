using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Weather.BL.Exceptions;
using Weather.BL.Services;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;
using Xunit;

namespace Weather.IntegrationTest.Service
{
    public class WeatherServiceIntegrationTest
    {
        private readonly WeatherService _weatherService;
        private readonly IWeatherRepository _weatherRepository;
        private readonly IValidator _validator;
        private readonly ConfigTest _configuratio;


        public  WeatherServiceIntegrationTest()
        {
            _configuratio = new ConfigTest();
            _weatherRepository = new WeatherRepository(_configuratio);
            _validator = new Validator();
            _weatherService = new WeatherService(_weatherRepository, _validator);
        }
       
        [Fact]
        public async Task GetWeatherAsync_CorrectWeatherIsReceived_IsErrorFalseAndMessageIsGenerated()
        {
            // Arrange
            var name = "Minsk";
            var model = await _weatherRepository.GetWeatherAsync(name);
            var message = $"In {model.Name}: {model.Main.Temp} ";

            //Act
            var response = await _weatherService.GetWeatherAsync(name);
            var messageResult = Regex.Split(response.Message, "°");

            // Assert
            Assert.False(response.IsError);
            Assert.Equal(message, messageResult[0]);
        }

        [Fact]
        public async Task GetWeatherAsync_ReceivedIncorrectWeather_IsErrorTrue()
        {
            // Arrange
            var name = "gdrh";
            _validator.ValidateCityByName(name);
            var message = $"{name} not found";
            //Act
            var response = await _weatherService.GetWeatherAsync(name);
            
            // Assert
            Assert.True(response.IsError);
            Assert.Equal(message, response.Message);
        }

        [Fact]
        public async Task GetWeatherAsync_ValidatorThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            void actualResult() => _validator.ValidateCityByName(name);
            await _weatherRepository.GetWeatherAsync(name);
            var message = "Entering the city is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>((actualResult));
            Assert.Equal(message, result.Message);
        }
    }
}
