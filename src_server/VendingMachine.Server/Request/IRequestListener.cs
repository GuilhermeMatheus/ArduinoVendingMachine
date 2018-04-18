using System.Threading.Tasks;

namespace VendingMachine.Server.Request
{
    public interface IRequestListener
    {
        void Listen();
        Task ListenAsync();
        void Start();
    }
}