using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Weather.BL.DTOs;

namespace Weather.BL.Services.Abstract
{
    public interface IWeatherHistoryService
    {
        //Task AddWeatherHistoryAsync(CityDTO city, CancellationTokenSource token);
        Task<List<WeatherHistoryDTO>> GetWeatherHistoriesAsync(string cityName, DateTime from, DateTime to);
        Task BackgroundSaveWeatherAsync(IEnumerable<string> cities);
    }
}
