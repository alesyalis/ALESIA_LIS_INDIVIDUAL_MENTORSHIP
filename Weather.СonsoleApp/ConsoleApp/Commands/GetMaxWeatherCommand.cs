using System;
using Weather.BL.Services.Abstract;
using ICommand = AppConfiguration.Interface.ICommand;
using System.Threading.Tasks;
using System.Threading;

namespace ConsoleApp.Commands
{
    public class GetMaxWeatherCommand : ICommand
    {

        private IWeatherService _weatherService;
        private CancellationTokenSource _token;
        public GetMaxWeatherCommand(IWeatherService weatherService, CancellationTokenSource token)
        {
            _weatherService = weatherService;
            _token = token; 
        }

        public string Title => "Find max temperature";

        public async Task Execute()
        {
            Console.WriteLine("Input city names:");
            var cities = Console.ReadLine();
            var cityNames = cities.Split(new string[] { " ", "," }, StringSplitOptions.RemoveEmptyEntries);

            var weather = await _weatherService.GetMaxWeatherAsync(cityNames, _token);

            Console.WriteLine(weather.Message);
        }
    }
}
