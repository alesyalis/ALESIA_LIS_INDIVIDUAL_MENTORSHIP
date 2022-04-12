﻿using AppConfiguration.Interface;
using ConsoleApp.Commands;
using ConsoleApp.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Weather.BL.Services.Abstract;

namespace ConsoleApp
{
    public class Program
    {
        private static IWeatherService _weatherService;

        private static List<ICommand> GetCommands()
        {
            var getWeather = new GetWeatherCommand(_weatherService);
            var getForecast = new GetForecastCommand(_weatherService);
            var exit = new ExitCommand();
            var token = new CancellationTokenSource();
            var getMaxTemperature = new GetMaxWeatherCommand(_weatherService, token);

            return new List<ICommand>()
            {
                exit, getWeather, getForecast, getMaxTemperature
            };
        }

        private static async Task Menu()
        {
            var commands = GetCommands();

            var flag = true;

            while (flag)
            {
                try
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\t\tWeather Forecast");
                    foreach (var command in commands)
                    {
                        Console.WriteLine($"{commands.IndexOf(command)}) " + command.Title);
                    }

                    var input = int.Parse(Console.ReadLine());
                    Console.Clear();

                    await commands[input].Execute();

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();

                }
                catch (Exception)
                {
                    Console.Clear();
                    Console.WriteLine("\t\t\t\t\tIncorrectly entered data!\n\nPlease, press any key to continue...");
                    Console.ReadKey();
                }

            }
        }

        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
            .AddRepositories().AddServices().BuildServiceProvider();

            _weatherService = serviceProvider.GetService<IWeatherService>();

            await Menu();
        }
    }
}