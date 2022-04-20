using AppConfiguration.AppConfig;
using AppConfiguration.Extentions;
using Ninject.Modules;

namespace Weather.СonsoleApp.Infrastructures
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
          //  Bind<IWeatherService>().To<WeatherService>();
            Bind<IConfig>().ToMethod(ctx =>
            {
                var config = new Config();
                config.PopulateConfigFromAppConfig();
                return config;
            }).InSingletonScope();
        }
    }
}
