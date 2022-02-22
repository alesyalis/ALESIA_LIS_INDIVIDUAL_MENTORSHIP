using System.Collections.Generic;

namespace Weather.BL.Validators.Abstract
{
    public interface IValidator
    {
        void ValidateCityByName(string value);

        void ValidateForecast(string name, int days);

        void ValidateCityNames(IEnumerable<string> cityNames);
    }
}
