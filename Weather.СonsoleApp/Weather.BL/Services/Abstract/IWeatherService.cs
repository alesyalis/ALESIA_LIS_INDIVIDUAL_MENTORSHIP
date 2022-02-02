using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherService
    {
        // Task<string> GetWeatherAsync(string cityName);
        Task<WeatherResponseNew> GetWeatherAsync(string cityName);
    }
}
