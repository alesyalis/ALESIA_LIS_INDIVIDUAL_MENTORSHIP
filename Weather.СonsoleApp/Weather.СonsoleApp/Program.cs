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
            GetWeatherCommand getWeatherCommand = new GetWeatherCommand(_weatherService);
            GetForecastCommand getForecastCommand = new GetForecastCommand(_weatherService);

            bool showMenu = true;
            while (showMenu)
            {
                switch (MainMenu())
                {
                    case 1:
                        await invoker.SetCommand(getWeatherCommand);
                        await invoker.Run();
                        break;
                    case 2:
                        await invoker.SetCommand(getForecastCommand);
                        await invoker.Run();
                        break;
                    case 3:
                        showMenu = false;
                        break;
                    default: break; 
                }
            }
        }

        private static int MainMenu()
        {
            var exit = 3;
            try
            {
                Console.WriteLine("Select an action:");
                Console.WriteLine("If you want to know the weather in the city, enter 1");
                Console.WriteLine("If you want to know the weather forecast in the city for the next few days, enter 2");
                Console.WriteLine("If you want to close the application, enter 3");

                return int.Parse(Console.ReadLine());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Press any key to exit!");   
                Console.ReadKey();
                return exit;
            }
            
        }
    }
}
