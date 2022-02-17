using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherService
    {
        Task<ResponseMessage> GetWeatherAsync(string cityName);

        Task<ResponseMessage> GetForecastAsync(string cityName, int days);
        Task<ResponseMessage> GetMaxWeatherAsync(List<string> cityName);
    }
}
