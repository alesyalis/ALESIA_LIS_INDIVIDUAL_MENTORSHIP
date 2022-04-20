using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherHistoryService
    {
        Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime dateTimeFrom, DateTime dateTimeTo);
         Task BackgroundSaveWeatherAsync(IEnumerable<string> cities);
    }
}
