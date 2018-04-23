using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Request
{
    public interface IRequestListener
    {
        void Listen();
        Task ListenAsync();
        void Start();
    }
}