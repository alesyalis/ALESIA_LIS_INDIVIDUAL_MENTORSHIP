using System;
using Weather.BL.Services.Abstract;
using ICommand = AppConfiguration.Interface.ICommand;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Weather.СonsoleApp.Commands
{
    public class GetMaxWeatherCommand : ICommand
    {

        private IWeatherService _weatherService;
        public GetMaxWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public string Title => "Show max weather";

        public async Task Execute()
        {
            Console.WriteLine("Enter number of cities");
            var count = Convert.ToInt32(Console.ReadLine());

            List<string> listCityName = new List<string>();


            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("Enter the name of the cities");
                listCityName.Add(Console.ReadLine());
            }

            var weather = await _weatherService.GetMaxWeatherAsync(listCityName);

            Console.WriteLine(weather.Message);
        }
    }
}
