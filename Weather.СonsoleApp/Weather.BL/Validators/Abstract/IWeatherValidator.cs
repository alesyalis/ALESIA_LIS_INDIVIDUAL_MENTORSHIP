using System;
using System.Collections.Generic;
using System.Text;

namespace Weather.BL.Validators.Abstract
{
    public interface IWeatherValidator
    {
        bool IsValidCityName(string cityName);
    }
}
