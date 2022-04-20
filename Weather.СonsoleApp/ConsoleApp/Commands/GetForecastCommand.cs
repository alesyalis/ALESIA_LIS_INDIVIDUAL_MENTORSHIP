using AppConfiguration.Interface;
using System;
using System.Threading.Tasks;
using Weather.BL.Services.Abstract;

namespace ConsoleApp.Commands
{
    public class GetForecastCommand : ICommand
    {
        private IWeatherService _weatherService;

        public GetForecastCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string Title => "Show forecast for several days";

        public async Task Execute()
        {
            Console.WriteLine("Enter the name of the city");
            var cityName = Console.ReadLine();
            Console.WriteLine("For how many days do you want to know the weather?");
            var day = Convert.ToInt32(Console.ReadLine());

            var weather = await _weatherService.GetForecastAsync(cityName, day);

            Console.WriteLine(weather.Message);
        }
    }
}
