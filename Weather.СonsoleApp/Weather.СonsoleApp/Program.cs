using Weather.BL.Validators;
using System;
using Weather.BL.Services;

namespace Weather.СonsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var weatherServices = new WeatherServicesv();

            while (true)
            {
                Console.WriteLine("Enter the name of the city");
                string city = Console.ReadLine();

                if (WeatherValidator.IsValidCityName(city))
                {
                    weatherServices.GetWeather(city);
                }
                else
                {
                    Console.WriteLine("Entering the city is required");
                }
            }
        }
    }
}
