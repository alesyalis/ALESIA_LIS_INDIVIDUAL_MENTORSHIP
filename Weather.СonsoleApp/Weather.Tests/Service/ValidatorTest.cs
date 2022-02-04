using NUnit.Framework;
using System;
using Weather.BL.Exceptions;
using Weather.BL.Validators.Abstract;

namespace Weather.Tests.Service
{
    public class ValidatorTest
    {
        private IValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new Validator();
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
    }
}
