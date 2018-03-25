using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server.Actions
{
    public interface IActionContextProvider
    {
        ActionContext GetContext(byte[] message);
    }
}
