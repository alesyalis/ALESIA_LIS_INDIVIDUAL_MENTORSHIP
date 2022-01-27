using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.BL.Services;
using Weather.BL.Services.Abstract;

namespace Weather.СonsoleApp.Infrastructures
{
    public class RegistrationModule: NinjectModule
    {
        public override void Load()
        {
            Bind<IWeatherService>().To<WeatherServicese>();
        }
    }
}
