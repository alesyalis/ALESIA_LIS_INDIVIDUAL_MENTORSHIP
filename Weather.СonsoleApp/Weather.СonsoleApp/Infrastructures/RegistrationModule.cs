using AppConfiguration.AppConfig;
using Ninject.Modules;
using Weather.BL.Services;
using Weather.BL.Services.Abstract;
using Configuration = AppConfiguration.AppConfig.Configuration;

namespace Weather.СonsoleApp.Infrastructures
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherService>();
            Bind<IConfiguration>().To<Configuration>().InSingletonScope();
        }
    }
}
