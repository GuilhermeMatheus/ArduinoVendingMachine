using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Actions
{
    public struct ActionContext
    {
        public ActionType Type { get; }
        public byte[] IncomingMessage { get; }

        public ActionContext(ActionType type, byte[] incomingMessage)
        {
            Type = type;
            IncomingMessage = incomingMessage;
        }
    }
}
