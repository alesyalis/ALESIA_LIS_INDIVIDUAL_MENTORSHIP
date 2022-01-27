using System;

namespace Weather.BL.Validators.Abstract
{
    public class Validator<T> : IValidator<T> where T : class
    {
        public void ValidateCityName(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "Entering the city is required");   
            }
        }
    }
}
