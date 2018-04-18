using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Services;
using VendingMachine.Server.ActionHandler;
using VendingMachine.Server.Exceptions;

namespace VendingMachine.Server.Actions
{
    public class ActionHandlerProvider : IActionHandlerProvider
    {
        private readonly SaleActionHandler _saleActionHandler;
        private readonly RepaymentActionHandler _repaymentActionHandler;
        private readonly MachineStartupActionHandler _machineStartupActionHandler;

        public ActionHandlerProvider(
            SaleActionHandler saleActionHandler,
            RepaymentActionHandler repaymentActionHandler,
            MachineStartupActionHandler machineStartupActionHandler)
        {
            _saleActionHandler = saleActionHandler ?? throw new ArgumentNullException(nameof(saleActionHandler));
            _repaymentActionHandler = repaymentActionHandler ?? throw new ArgumentNullException(nameof(repaymentActionHandler));
            _machineStartupActionHandler = machineStartupActionHandler ?? throw new ArgumentNullException(nameof(machineStartupActionHandler));
        }

        public IActionHandler GetActionHandler(ActionContext actionContext)
        {
            switch (actionContext.Type)
            {
                case ActionType.Sale:
                    return _saleActionHandler;

                case ActionType.Repayment:
                    return _repaymentActionHandler;

                case ActionType.MachineStartup:
                    return _machineStartupActionHandler;

                case ActionType.PriceUpdate:
                default:
                    throw new ActionNotSupportedException((byte)actionContext.Type);
            }
        }
    }
}
