using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using Weather.DataAccess.Repositories;
using Weather.DataAccess.Repositories.Abstrdact;

namespace Weather.BL.Infrastructures
{
    public class RegistrationModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherRepository>().To<WeatherRepository>();
        }
    }
}
