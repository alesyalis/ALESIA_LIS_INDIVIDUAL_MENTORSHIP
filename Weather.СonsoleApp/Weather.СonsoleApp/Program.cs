using System;
using System.Threading.Tasks;
using Ninject.Modules;
using Weather.BL.Infrastructures;
using Ninject;
using System.Reflection;
using System.Web.Mvc;
using Ninject.Web.Mvc;
using Weather.BL.Services.Abstract;
using AppConfiguration.AppConfig;
using System.Configuration;
using Weather.BL.Exceptions;

namespace Weather.СonsoleApp
{
    public class Program 
    {
        private static IWeatherService _weatherService;
        static async Task Main(string[] args)
        {
            NinjectModule serviceModule = new RegistrationModule();

            var kernel = new StandardKernel(serviceModule);
            kernel.Load(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            _weatherService = kernel.Get<IWeatherService>();

            while (true)
            {
                try
                {
                   Console.WriteLine("Enter the name of the city");
                   var cityName = Console.ReadLine();
                   var weather = await _weatherService.GetWeatherAsync(cityName);
                   Console.WriteLine("В {0}: {1} °C {2} ", weather.Name, weather.Main.Temp, weather.Main.Description);
                }
                catch (ArgumentNullException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (Exception)
                {
                    Console.WriteLine("City not found\n");
                }
            }
        }
    }
}
