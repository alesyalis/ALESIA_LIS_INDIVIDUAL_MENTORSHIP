using System.Threading.Tasks;

namespace Weather.BL.Validators
{
    public class WeatherValidator
    {
        public bool IsValidCityName(string cityName)
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
