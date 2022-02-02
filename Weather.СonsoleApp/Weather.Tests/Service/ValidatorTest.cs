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
        public void ValidateCityByName_IfStringEmpty_ValidationIsFaild(string name)
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => _validator.ValidateCityByName(name));
        }
        [Test]
        public void ValidateCityByName_IfStringEmpty_ValidationIsFaild()
        {
            // Arrange
            var name = string.Empty;    

            // Act
            void result () => _validator.ValidateCityByName(name);

            // Assert
            Assert.Throws<ValidationException>(result);
        }

        [Test]
        public void ValidateCityByName_IfStringEmpty_ValidationMessage()
        {
            // Arrange
            var name = string.Empty;
            var message = "Entering the city is required\n";

            // Act
            void result() => _validator.ValidateCityByName(name);
            Exception ex = Assert.Throws<ValidationException>(result);

            // Assert
            Assert.AreEqual(message, ex.Message);
        }
    }
}
