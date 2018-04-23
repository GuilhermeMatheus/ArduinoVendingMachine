using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Infrastructure.Request;

namespace VendingMachine.Infrastructure.Actions
{
    public class ActionContextProvider : IActionContextProvider
    {
        public ActionContext GetContext(RequestData data) =>
            new ActionContext(
                (ActionType)data.RawBytes[0],
                data
            );
    }
}
