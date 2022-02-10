using System;
using System.Threading.Tasks;
using Ninject.Modules;
using Weather.BL.Infrastructures;
using Ninject;
using System.Reflection;
using System.Web.Mvc;
using Ninject.Web.Mvc;
using Weather.BL.Services.Abstract;
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

            bool showMenu = true;
            while (showMenu)
            {
                showMenu = await MainMenu();
            }
        }

        private static async Task<bool> MainMenu()
        {
            Console.WriteLine("Select an action:");
            Console.WriteLine("If you want to know the weather in the city, enter 1");
            Console.WriteLine("If you want to close the application, enter 2");
            Console.WriteLine("If you want to know the weather forecast in the city for the next few days, enter 3");

            switch (Console.ReadLine())
            {
                case "1":
                    await GetWeatherByCityNameAsync();
                    return true;
                case "2":
                    return false;
                case "3":
                    await GetForecastByCityNameAsync();
                    return true;
                default:
                    return true;
            }
        }

        private static async Task GetWeatherByCityNameAsync()
        {
            try
            {
                Console.WriteLine("Enter the name of the city");
                var cityName = Console.ReadLine();

                var weather = await _weatherService.GetWeatherAsync(cityName);
                Console.WriteLine(weather.Message);


            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhand exeption :" + ex.Message);
            }
        }
        private static async Task GetForecastByCityNameAsync()
        {
            try
            {
                Console.WriteLine("Enter the name of the city");
                var cityName = Console.ReadLine();
                Console.WriteLine("For how many days do you want to know the weather?");
                var day = Convert.ToInt32(Console.ReadLine());

                var weather = await _weatherService.GetForecastAsync(cityName, day);
               // foreach (var temp in weather)
               // {
                    Console.WriteLine(weather.Message);

               // }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhand exeption :" + ex.Message);
            }
        }
    }
}
