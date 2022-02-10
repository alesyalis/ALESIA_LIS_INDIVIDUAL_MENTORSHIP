using AppConfiguration.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Weather.СonsoleApp.Invoker
{
    public class WeatherInvoker
    {
        ICommand command;
        
        public async Task SetCommand(ICommand com)
        {
            command = com;
        }

        public async Task Run()
        {
          await  command.Execute();  
        }
    }
}
