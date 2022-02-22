using AppConfiguration.AppConfig;
using System.Collections.Generic;
using System.Linq;
using Weather.BL.Exceptions;

namespace Weather.BL.Validators.Abstract
{
    public class Validator: IValidator
    {
        private readonly IConfig _configuration;
        public Validator(IConfig cong)
        {
            _configuration = cong;  
        }
        public void ValidateCityByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ValidationException("Entering the city is required\n");
            }
        }

        public void ValidateForecast(string name, int days)
        {
            ValidateCityByName(name);

            if (days <= _configuration.Days)
            {
                throw new ValidationException("Input number of days is required\n");
            }
        }

        public void ValidateCityNames(IEnumerable<string> cityNames)
        {
            if (cityNames.Count() == 0 || cityNames == null)
            {
                throw new ValidationException("Entering the city is required\n");
            }
        }
    }
}
