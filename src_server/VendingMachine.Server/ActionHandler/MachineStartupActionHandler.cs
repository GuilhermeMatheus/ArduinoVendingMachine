using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server.ActionHandler
{
    public class MachineStartupActionHandler : IActionHandler
    {
        public MachineStartupActionHandler( /* Serviço que atualiza IP da máquina */ ) 
        {

        }


        public byte[] Process(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
