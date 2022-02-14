﻿using AppConfiguration.Interface;
using System;
using System.Threading.Tasks;

namespace Weather.СonsoleApp.Commands
{
    public class ExitCommand : ICommand
    {
        public string Title => "Exist";

        public Task Execute()
        {
            Environment.Exit(0);
            return Task.CompletedTask;  
        }
    }
}
