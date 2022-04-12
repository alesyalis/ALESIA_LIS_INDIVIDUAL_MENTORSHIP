using Microsoft.Extensions.DependencyInjection;
using Weather.BL.Services.Abstract;
using Weather.BL.Services;
using Weather.BL.Validators.Abstract;
using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;

namespace ConsoleApp.Extensions
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services) 
        {
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IValidator, Validator>();
            services.AddSingleton<IConfig>(context => 
            {
                var config = new Config();
                config.GetConfig();

                return config;
            });

            return services;
        }
    }
}
