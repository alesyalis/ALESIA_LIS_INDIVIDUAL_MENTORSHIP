using System.Threading.Tasks;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public interface IWeatherRepository
    {
        Task<WeatherResponse> GetWeatherAsync(string cityName);
    }
}
