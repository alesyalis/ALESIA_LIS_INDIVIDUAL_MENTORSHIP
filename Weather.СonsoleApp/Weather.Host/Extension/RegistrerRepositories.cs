using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.Host.Extension
{
    public static class RegistrerRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherRepository, WeatherRepository>();
            services.AddSingleton<IWeatherHistoryRepository, WeatherHistoryRepository>();

            return services;
        }
    }
}
