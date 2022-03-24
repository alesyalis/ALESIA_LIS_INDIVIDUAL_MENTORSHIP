using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;
using Weather.BL.Services;
using Weather.BL.Services.Abstract;

namespace Weather.WebAPI.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfig>(context =>
            {
                var config = new Config();
                config.PopulateConfigFromAppConfig();
                return config;
            });
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }
    }
 }
