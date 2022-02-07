﻿using System.Threading.Tasks;
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
        private WeatherService _weatherService;
        private IWeatherRepository _weatherRepository;
        private IValidator _validator;
        private IConfigTest _configuratio;


        public  WeatherServiceIntegrationTest()
        {
            _configuratio = new IConfigTest();
            _weatherRepository = new WeatherRepository(_configuratio);
            _validator = new Validator();
            _weatherService = new WeatherService(_weatherRepository, _validator);
        }
       
        [Fact]
        public async Task GetWeatherAsync_CorrectWeatherIsReceived_IsErrorFalseAndMessageIsGenerated()
        {
            // Arrange
            var name = "Minsk";
            _validator.ValidateCityByName(name);
            var model = await _weatherRepository.GetWeatherAsync(name);
            var message = $"In {model.Name}: {model.Main.Temp} ";

            //Act
            var response = await _weatherService.GetWeatherAsync(name);
            var messageResult = response.Message.Substring(0, response.Message.IndexOf('°'));
            // Assert
            Assert.False(response.IsError);
            Assert.Equal(message, messageResult);
        }

        [Fact]
        public async Task GetWeatherAsync_ReceivedIncorrectWeather_IsErrorTrue()
        {
            // Arrange
            var name = "gdrh";
            _validator.ValidateCityByName(name);
            await _weatherRepository.GetWeatherAsync(name);

            //Act
            var response = await _weatherService.GetWeatherAsync(name);
            
            // Assert
            Assert.True(response.IsError);
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
