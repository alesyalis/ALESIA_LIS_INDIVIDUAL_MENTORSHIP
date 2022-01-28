using System;

namespace Weather.BL.Validators.Abstract
{
    public class Validator: IValidator
    {
        public void ValidateCityByNameName(string name)
        {
            if (name == string.Empty)
            {
                throw new ArgumentNullException("Entering the city is required\n");
            }
        }
    }
}
