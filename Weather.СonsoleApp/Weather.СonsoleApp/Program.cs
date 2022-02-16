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

            var getWeather = new GetWeatherCommand(_weatherService);
            var getForecast = new GetForecastCommand(_weatherService);
            var exit = new ExitCommand();

            var listCommand = new List<ICommand>()
            {
                exit, getWeather, getForecast, 
            };

            bool showMenu = true;
            while (showMenu)
            {
                try
                {
                    foreach (var command in listCommand)
                    {
                        Console.WriteLine($"{listCommand.IndexOf(command)} - " + command.Title);
                    }

                    int number = int.Parse(Console.ReadLine());
                 
                    await listCommand[number].Execute();
                }
                catch (ValidationException ex)
                {
                    Console.WriteLine(ex.Message);
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
