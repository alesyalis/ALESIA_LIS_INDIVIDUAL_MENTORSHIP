using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.DataAccess.Configuration;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.DataAccess.Repositories
{
    public class WeatherHistoryRepository : BaseRepository<WeatherHistory>, IWeatherHistoryRepository
    {
        public WeatherHistoryRepository(ApplicationDbContext context) : base(context)
        {

        }
        public async Task<List<WeatherHistory>> GetWeatherHistoriesAsync(string citiName, DateTime dateTimeFrom, DateTime dateTimeTo)
        {
            return await _context.WeatherHistories.AsNoTracking()
                .Where(x => x.City.CityName.ToLower() == citiName.ToLower()
                         && x.Timestamp.Date >= dateTimeFrom
                         && x.Timestamp.Date <= dateTimeTo)
                .ToListAsync(); 
        }
    }
}
