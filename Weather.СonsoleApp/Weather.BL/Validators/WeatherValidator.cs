using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.BL.Validators
{
    public class WeatherValidator
    {
        public static bool IsValidCityName(string cityName)
        {
            if (cityName != string.Empty)
            {
                return true;
            }
            else
            {
                return false;

            }
        }
    }
}
