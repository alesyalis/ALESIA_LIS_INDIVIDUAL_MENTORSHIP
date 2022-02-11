using System;
using System.Threading.Tasks;
using Ninject.Modules;
using Weather.BL.Infrastructures;
using Ninject;
using System.Reflection;
using System.Web.Mvc;
using Ninject.Web.Mvc;
using Weather.BL.Services.Abstract;
using Weather.СonsoleApp.Invoker;
using Weather.СonsoleApp.Commands;
using AppConfiguration.Interface;

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

            WeatherInvoker invoker = new WeatherInvoker();

            ICommand getWeather = new GetWeatherCommand(_weatherService);
            ICommand getForecast = new GetForecastCommand(_weatherService);

           
            Switch sw = new Switch();

            bool showMenu = true;
            while (showMenu)
            {
                try
                {
                    Console.WriteLine("Select the desired case : 1 - Weather, 2 - Forecast, 3 - Exit");
                    int number = int.Parse(Console.ReadLine());

                    switch (number)
                    {
                        case 1: await sw.StoreAndExecute(getWeather); break;
                        case 2: await sw.StoreAndExecute(getForecast); break;
                        case 3: showMenu = false; break;
                        default: break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("Press any key to exit!");
                    Console.ReadKey();
                }
            }
        }
    }
}
