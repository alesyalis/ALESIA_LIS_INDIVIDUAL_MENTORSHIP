using NUnit.Framework;
using Weather.BL.Exceptions;
using Weather.BL.Validators.Abstract;

namespace Weather.Tests.Service
{
    public class ValidatorTest
    {
        [Test]
        public void ValidateCityByName_IfStringEmpty_ValidationIsFaild()
        {
            var name = "";
            var sut = new Validator();

            NUnit.Framework.Assert.Throws<ValidationException>(() => sut.ValidateCityByName(name));
        }
    }
}
