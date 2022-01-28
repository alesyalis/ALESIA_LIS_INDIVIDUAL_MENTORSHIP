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

namespace Weather.СonsoleApp
{
    public class Program 
    {
        private static IWeatherService _weatherService;
        private static IConfiguration _configuration;
        static async Task Main(string[] args)
        {
            NinjectModule serviceModule = new RegistrationModule();

            var kernel = new StandardKernel(serviceModule);
            kernel.Load(Assembly.GetExecutingAssembly());
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
            _weatherService = kernel.Get<IWeatherService>();
            _configuration = kernel.Get<IConfiguration>();
            
            _configuration.Api = ConfigurationManager.AppSettings["api"];
            _configuration.ApiKey = ConfigurationManager.AppSettings["apiKey"];    

            while (true)
            {
                try
                {
                   Console.WriteLine("Enter the name of the city");
                   var cityName = Console.ReadLine();
                   var weather = await _weatherService.GetWeatherAsync(cityName);
                   Console.WriteLine("В {0}: {1} °C {2} ", weather.Name, weather.Main.Temp, weather.Main.Description);
                }
                catch (Exception)
                {
                    Console.WriteLine("City not found\n");
                }
            }
        }
    }
}
