using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Exceptions
{
    public class ActionNotSupportedException : Exception
    {
        public ActionNotSupportedException(byte action) 
            : base(GetMessage(action))
        {
        }

        public ActionNotSupportedException(byte action, Exception innerException)
            : base(GetMessage(action), innerException)
        {
        }

        private static string GetMessage(byte action) => 
            $"Action {action} not supported";
    }
}
