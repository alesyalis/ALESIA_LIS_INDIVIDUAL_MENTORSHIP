using System;
using Weather.BL.Services.Abstract;
using ICommand = AppConfiguration.Interface.ICommand;
using System.Threading.Tasks;
using Weather.BL.Exceptions;

namespace Weather.СonsoleApp.Commands
{
    public class GetWeatherCommand : ICommand
    {

        private IWeatherService _weatherService;
        public GetWeatherCommand(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }
        public async Task Execute()
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
    }
}
