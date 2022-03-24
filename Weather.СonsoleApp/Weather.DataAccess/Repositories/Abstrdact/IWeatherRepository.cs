using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public interface IWeatherRepository
    {
        Task<WeatherResponse> GetWeatherAsync(string cityName, CancellationTokenSource token);
        Task<ForecastResponse> GetForecastAsync(string cityName, int days);

        Task<IEnumerable<WeatherResponse>> GetListWeatherAsync(IEnumerable<string> cityName, CancellationTokenSource token);
    }
}
