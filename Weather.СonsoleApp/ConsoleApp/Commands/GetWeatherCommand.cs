using System;
using Weather.BL.Services.Abstract;
using ICommand = AppConfiguration.Interface.ICommand;
using System.Threading.Tasks;

namespace ConsoleApp.Commands
{
    public class GetWeatherCommand : ICommand
    {

        private IWeatherService _weatherService;
        public GetWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string Title => "Show current forecast";

        public async Task Execute()
        {
            Console.WriteLine("Enter the name of the city");
            var cityName = Console.ReadLine();

            var weather = await _weatherService.GetWeatherAsync(cityName);

            Console.WriteLine(weather.Message);
        }
    }
}
