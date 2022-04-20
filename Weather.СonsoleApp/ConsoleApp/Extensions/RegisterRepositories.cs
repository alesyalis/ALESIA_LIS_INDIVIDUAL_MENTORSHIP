using Microsoft.Extensions.DependencyInjection;
using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace ConsoleApp.Extensions
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
