using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Infrastructure.Actions;
using VendingMachine.Infrastructure.Request;

namespace VendingMachine.Infrastructure.Actions
{
    public interface IActionContextProvider
    {
        ActionContext GetContext(RequestData message);
    }
}
