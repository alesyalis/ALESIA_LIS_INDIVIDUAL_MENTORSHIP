using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.BL.Validators;

namespace Weather.СonsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                System.Console.WriteLine("Enter the name of the city");
                string city = System.Console.ReadLine();

                if (WeatherValidator.IsValidCityName(city))
                {
                    BL.Services.Weather.GetWeather(city);
                }
                else
                {
                    System.Console.WriteLine("Entering the city is required");
                }
            }
        }
    }
}
