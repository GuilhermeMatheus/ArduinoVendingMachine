using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Infrastructure.Actions
{
    public enum ActionType : byte
    {
        Sale = 0x00,
        Repayment = 0x01,
        PriceUpdate = 0x02,
        MachineStartup = 0x03
    }
}
