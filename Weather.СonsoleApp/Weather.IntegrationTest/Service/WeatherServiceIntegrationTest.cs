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
        private readonly ConfigTest _configuration;


        public  WeatherServiceIntegrationTest()
        {
            _configuration = new ConfigTest();
            _weatherRepository = new WeatherRepository(_configuration);
            _validator = new Validator();
            _weatherService = new WeatherService(_weatherRepository, _validator);
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
            var temp = $"^In {name}: {regex} °C ({description1}|{description2}|{description3}|{description4})$";

            //Act
            var response = await _weatherService.GetWeatherAsync(name);
            
           // Assert
            Assert.False(response.IsError);
            Assert.Matches(temp, response.Message);
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
        public async Task GetWeatherAsync_ValidatorThrowsIsExeption_ReceivedError()
        {
            // Arrange
            var name = "";
            var message = "Entering the city is required\n";

            //Act
            // Assert
            var result = Assert.Throws<ValidationException>(() => _validator.ValidateCityByName(name));
            Assert.Equal(message, result.Message);
        }
    }
}
