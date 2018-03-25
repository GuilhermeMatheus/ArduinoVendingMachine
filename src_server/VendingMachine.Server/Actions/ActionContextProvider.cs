using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Actions
{
    public class ActionContextProvider : IActionContextProvider
    {
        public ActionContext GetContext(byte[] message) =>
            new ActionContext(
                (ActionType)message[0],
                message
            );
    }
}
