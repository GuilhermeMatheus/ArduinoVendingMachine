using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Request;

namespace VendingMachine.Server.Actions
{
    public class ActionContext
    {
        public ActionType Type { get; }
        public RequestData IncomingMessage { get; }

        public ActionContext(ActionType type, RequestData incomingMessage)
        {
            Type = type;
            IncomingMessage = incomingMessage;
        }
    }
}
