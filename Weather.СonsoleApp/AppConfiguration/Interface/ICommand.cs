using System.Threading.Tasks;

namespace AppConfiguration.Interface
{
    public interface ICommand
    {
        string Title { get; }  
        Task Execute();
    }
}
