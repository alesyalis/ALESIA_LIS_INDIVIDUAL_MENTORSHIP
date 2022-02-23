using System;
using Weather.BL.Services.Abstract;
using ICommand = AppConfiguration.Interface.ICommand;
using System.Threading.Tasks;

namespace Weather.СonsoleApp.Commands
{
    public class GetMaxWeatherCommand : ICommand
    {

        private IWeatherService _weatherService;
        public GetMaxWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string Title => "Find max temperature";

        public async Task Execute()
        {
            Console.WriteLine("Input city names:");
            var cities = Console.ReadLine();
            var cityNames = cities.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

            var weather = await _weatherService.GetMaxWeatherAsync(cityNames);

            Console.WriteLine(weather.Message);
        }
    }
}
