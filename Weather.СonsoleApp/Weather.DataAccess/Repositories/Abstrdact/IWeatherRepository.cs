using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public interface IWeatherRepository
    {
        Task<WeatherResponse> GetWeatherAsync(string cityName);
        Task<ForecastResponse> GetForecastAsync(string cityName, int days);

        Task<List<WeatherResponse>> GetListWeatherAsync(List<string> cityName);
    }
}
