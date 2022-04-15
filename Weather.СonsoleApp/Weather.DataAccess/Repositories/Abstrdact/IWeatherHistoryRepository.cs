using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Repositories.Abstrdact
{
    public interface IWeatherHistoryRepository : IBaseRepository<WeatherHistory>
    {
        Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string citiName, DateTime dateTimeFrom, DateTime dateTimeTo);
        Task BalkSaveWeatherAsync(IEnumerable<WeatherHistory> weatherHistories);
    }
}