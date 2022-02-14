using System;
using System.Threading.Tasks;
using Ninject.Modules;
using Weather.BL.Infrastructures;
using Ninject;
using System.Reflection;
using System.Web.Mvc;
using Ninject.Web.Mvc;
using Weather.BL.Services.Abstract;
using Weather.СonsoleApp.Commands;
using AppConfiguration.Interface;
using System.Collections.Generic;

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

            ICommand getWeather = new GetWeatherCommand(_weatherService);
            ICommand getForecast = new GetForecastCommand(_weatherService);
            ICommand exit = new ExitCommand();

            var listCommand = new List<ICommand>()
            {
                getWeather, getForecast, exit
            };

            bool showMenu = true;
            while (showMenu)
            {
                try
                {
                    foreach (var command in listCommand)
                    {
                        Console.WriteLine(command.Title);
                    }

                    int number = int.Parse(Console.ReadLine());
                 
                    await listCommand[number].Execute();
                }
                catch (Exception)
                {
                    Console.WriteLine("Press any key to exit!");
                    Console.ReadKey();
                }
            }
        }
    }
}
