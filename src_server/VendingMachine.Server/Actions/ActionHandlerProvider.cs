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
        private readonly ISaleService _saleService;

        public ActionHandlerProvider(ISaleService saleService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        public IActionHandler GetActionHandler(ActionContext actionContext)
        {
            switch (actionContext.Type)
            {
                case ActionType.Sale:
                    return new SaleActionHandler(_saleService);

                case ActionType.Repayment:
                    return new RepaymentActionHandler();

                case ActionType.MachineStartup:
                    return new MachineStartupActionHandler();

                case ActionType.PriceUpdate:
                default:
                    throw new ActionNotSupportedException((byte)actionContext.Type);
            }
        }
    }
}
