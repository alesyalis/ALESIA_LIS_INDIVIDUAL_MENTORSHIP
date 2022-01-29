using AppConfiguration.AppConfig;
using Ninject.Modules;
using Weather.BL.Services;
using Weather.BL.Services.Abstract;


namespace Weather.СonsoleApp.Infrastructures
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherService>();
            Bind<IConfig>().To<Config>().InSingletonScope();

        }
    }
}
