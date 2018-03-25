using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.ActionHandler;

namespace VendingMachine.Server.Actions
{
    public class ActionHandlerProvider : IActionHandlerProvider
    {
        public IActionHandler GetActionHandler(ActionContext actionContext)
        {
            switch (actionContext.Type)
            {
                case ActionType.Sale:
                    return new SaleActionHandler();

                case ActionType.Repayment:
                    return new RepaymentActionHandler();

                case ActionType.PriceUpdate:
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
