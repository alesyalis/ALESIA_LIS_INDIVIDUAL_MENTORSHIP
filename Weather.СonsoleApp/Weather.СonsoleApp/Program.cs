using System;
using Weather.BL.Services;
using System.Threading.Tasks;

namespace Weather.СonsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var key = System.Configuration.ConfigurationManager.AppSettings["apiKey"];

            var weatherServices = new WeatherServicese();

            while (true)
            {
                try
                {
                    Console.WriteLine("Enter the name of the city");

                    var weather = await weatherServices.GetWeatherAsync(key);

                    Console.WriteLine("В {0}: {1} °C {2} ", weather.Name, weather.Main.Temp, weather.Main.Description);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("City not found\n" + ex.Message);
                }
            }
        }
    }
}
