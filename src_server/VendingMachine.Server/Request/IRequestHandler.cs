using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Request
{
    public interface IRequestHandler
    {
        byte[] GetRequestBytes();
        Task SendResponse(byte[] data);
    }
}
