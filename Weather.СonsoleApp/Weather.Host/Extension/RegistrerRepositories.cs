using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.Host.Extension
{
    public static class RegistrerRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();
            services.AddScoped<IWeatherHistoryRepository, WeatherHistoryRepository>();

            return services;
        }
    }
}
