using System.Threading.Tasks;

namespace AppConfiguration.Interface
{
    public interface ICommand
    {
        Task Execute();
    }
}
