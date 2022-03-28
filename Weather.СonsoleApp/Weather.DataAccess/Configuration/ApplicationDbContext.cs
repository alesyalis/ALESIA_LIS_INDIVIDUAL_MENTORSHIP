using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Weather.DataAccess.Models;

namespace Weather.DataAccess.Configuration
{
    public class ApplicationDbContext : DbContext
    {
        private readonly ILoggerFactory _loggerFactory;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory; 
        }

        public DbSet<WeatherHistory> WeatherHistories { get; set; }
        public DbSet<City> Cities { get; set; } 

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseLoggerFactory(_loggerFactory);
        }
    }
}
