using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Weather.BL.Exceptions;
using Weather.BL.Validators.Abstract;

namespace Weather.Tests.Service
{
    public class ValidatorTest
    {
        private IValidator _validator;
        private ConfigTest _configuration;

        [SetUp]
        public void Setup()
        {
            _configuration = new ConfigTest();
            _validator = new Validator(_configuration);
        }

        [TestCase("")]
        [TestCase(null)]
        public void ValidateCityByName_CityNameIsEmpty_ValidationMessage(string name)
        {
            // Arrange
            var message = "Entering the city is required\n";

            // Act
            void result() => _validator.ValidateCityByName(name);

            // Assert
            Exception ex = Assert.Throws<ValidationException>(result);
            Assert.AreEqual(message, ex.Message);
        }

        [TestCase("Minsk")]
        [TestCase("string")]
        public void ValidateCityByName_CityNameReceived_Success(string name)
        {
            // Arrange
            // Act
            void result() => _validator.ValidateCityByName(name);

            // Assert
            Assert.DoesNotThrow(result);
        }

        [TestCase("Minsk", 2)]
        [TestCase("string", 5)]
        public void ValidateForecast_CityNameReceived_Success(string name, int days)
        {
            // Arrange
            // Act
            void result() => _validator.ValidateForecast(name, days);

            // Assert
            Assert.DoesNotThrow(result);
        }

        [TestCase("", 6)]
        [TestCase(null, 0)]
        public void ValidateForecast_CityNameIsEmpty_ValidationMessage(string name, int days)
        {
            // Arrange
            var message = "Entering the city is required\n";

            // Act
            void result() => _validator.ValidateForecast(name, days);

            // Assert
            Exception ex = Assert.Throws<ValidationException>(result);
            Assert.AreEqual(message, ex.Message);
        }

        [TestCase("Minsk", 0)]
        public void ValidateForecast_DaysIsEmpty_ValidationMessage(string name, int days)
        {
            // Arrange
            var message = "Input number of days is required\n";

            // Act
            void result() => _validator.ValidateForecast(name, days);

            // Assert
            Exception ex = Assert.Throws<ValidationException>(result);
            Assert.AreEqual(message, ex.Message);
        }

        [Test]
        public void ValidateForecast_CityNamesIsEmpty_ValidationMessage()
        {
            // Arrange
            var message = "Entering the city is required\n";
            var names = new List<string>() {  };

            // Act
            void result() => _validator.ValidateCityNames(names);

            // Assert
            Exception ex = Assert.Throws<ValidationException>(result);
            Assert.AreEqual(message, ex.Message);
        }

        [Test]
        public void ValidateCityByName_CityNamesReceived_Success()
        {
            // Arrange
            var names = new List<string>() { "Minsk", "London" };

            // Act
            void result() => _validator.ValidateCityNames(names);

            // Assert
            Assert.DoesNotThrow(result);
        }
    }
}
