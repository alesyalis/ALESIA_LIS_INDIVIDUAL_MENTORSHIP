using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;
using Weather.BL.Services;
using Weather.BL.Services.Abstract;
using Weather.BL.Validators.Abstract;

namespace Weather.Host.Extension
{
    public static class RegisterServices
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSingleton<IConfig>(context =>
            {
                var config = new Config();
                config.GetConfig();

                return config;
            });
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IValidator, Validator>();
            services.AddScoped<IWeatherHistoryService, WeatherHistoryService>();
            services.AddScoped<IBackgroundJobService, BackgroundJobService>();

            return services;
        }
    }
}
