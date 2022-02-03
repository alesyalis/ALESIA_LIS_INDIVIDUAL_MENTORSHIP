using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherService
    {
        Task<WeatherResponseMessage> GetWeatherAsync(string cityName);
    }
}
