using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Actions
{
    public interface IActionHandlerProvider
    {
        IActionHandler GetActionHandler(ActionContext actionContext);
    }
}
