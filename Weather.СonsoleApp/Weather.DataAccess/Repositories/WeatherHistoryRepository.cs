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
                .Where(x => x.CityName.ToLower() == citiName.ToLower()
                         && x.DateTime.Date >= dateTimeFrom
                         && x.DateTime.Date <= dateTimeTo)
                .ToListAsync(); 
        }


        public async Task BalkSaveWeatherAsync(IEnumerable<WeatherHistory> weatherHistories)
        {
            await _context.WeatherHistories.AddRangeAsync(weatherHistories);  
            await _context.SaveChangesAsync();  
        }
    }
}
