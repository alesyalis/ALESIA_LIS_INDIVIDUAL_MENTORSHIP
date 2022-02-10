using AppConfiguration.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Weather.СonsoleApp.Commands
{
    public class Switch
    {
        private List<ICommand> _commands = new List<ICommand>();
        public async Task StoreAndExecute(ICommand command)
        {
            _commands.Add(command);
           await command.Execute();
        }

    }
}
