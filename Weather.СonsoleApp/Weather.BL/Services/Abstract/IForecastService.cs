using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IForecastService
    {
        Task<List<ForecastResponseMessage>> GetForecastAsync(string cityName, int days);

    }
}
