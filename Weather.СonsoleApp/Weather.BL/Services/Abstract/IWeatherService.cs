using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherService
    {
        Task<ResponseMessageDTO> GetWeatherAsync(string cityName);

        Task<ResponseMessageDTO> GetForecastAsync(string cityName, int days);
        Task<ResponseMessageDTO> GetMaxWeatherAsync(IEnumerable<string> cityName, CancellationTokenSource token);
    }
}
