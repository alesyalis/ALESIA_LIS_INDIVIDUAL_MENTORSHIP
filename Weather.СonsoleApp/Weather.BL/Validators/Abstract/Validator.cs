using Weather.BL.Exceptions;

namespace Weather.BL.Validators.Abstract
{
    public class Validator: IValidator
    {
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

            if (days <= 0)
            {
                throw new ValidationException("Input number of days is required\n");
            }
        }
    }
}
