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
                    var cityName = Console.ReadLine();

                    var weather = await weatherServices.GetWeatherAsync(cityName);
                    if (weather.ErrorMessage != string.Empty)
                    {
                        Console.WriteLine(weather.ErrorMessage);
                    }
                    else
                    {
                        Console.WriteLine("В {0}: {1} °C {2} ", weather.Name, weather.Main.Temp, weather.Main.Description);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("City not found\n");
                }
            }
        }
    }
}
