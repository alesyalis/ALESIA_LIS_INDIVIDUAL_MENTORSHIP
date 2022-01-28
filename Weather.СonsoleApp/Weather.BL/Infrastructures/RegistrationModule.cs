using Ninject.Modules;
using Weather.BL.Validators;
using Weather.BL.Validators.Abstract;
using Weather.DataAccess.Models;
using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Infrastructures
{
    public class RegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherRepository>().To<WeatherRepository>();
            Bind<IValidator<WeatherResponse>>().To<WeatherValidator>();
        }
    }
}
