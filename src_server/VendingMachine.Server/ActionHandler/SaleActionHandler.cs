using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Services;
using VendingMachine.Server.Actions;
using VendingMachine.Server.Sale;

namespace VendingMachine.Server.ActionHandler
{
    public class SaleActionHandler : IActionHandler
    {
        private readonly ISaleService _saleService;

        public SaleActionHandler(ISaleService saleService)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
        }

        public byte[] Process(ActionContext context)
        {
            var data = context.IncomingMessage;

            var sri = SaleRequestInfoParser.Parse(data.RawBytes);
            var saleOperation = new SaleOperation(
                machineId: sri.MachineId,
                clientCardId: sri.ClientCardId,
                itemsCount: sri.ItemsCount,
                itemsId: sri.ItemsId,
                price: sri.Price
            );

            var saleResult = _saleService.Sale(saleOperation);

            return SaleRequestInfoParser.ParseResponse(saleResult);
        }
    }
}