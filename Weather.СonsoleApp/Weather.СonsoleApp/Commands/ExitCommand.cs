using AppConfiguration.Interface;
using System;
using System.Threading.Tasks;

namespace Weather.СonsoleApp.Commands
{
    public class ExitCommand : ICommand
    {
        public string Title => "Exist 2";

        public Task Execute()
        {
            Environment.Exit(0);
            return Task.CompletedTask;  
        }
    }
}
