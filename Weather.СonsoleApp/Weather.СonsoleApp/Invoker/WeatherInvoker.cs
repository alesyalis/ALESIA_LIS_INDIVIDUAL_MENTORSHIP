using AppConfiguration.Interface;
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
