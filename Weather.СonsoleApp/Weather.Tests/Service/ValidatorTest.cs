using NUnit.Framework;
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
        [Test]
        public void ValidateCityByName_IfStringEmpty_ValidationIsFaild(string name)
        {
            // Arrange

            // Act

            // Assert
            Assert.Throws<ValidationException>(() => _validator.ValidateCityByName(name));
        }
    }
}
