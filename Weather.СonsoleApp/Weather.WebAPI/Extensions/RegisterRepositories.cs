using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.WebAPI.Extensions
{
    public static class RegisterRepositories
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IWeatherRepository, WeatherRepository>();

            return services;
        }
    }
}
