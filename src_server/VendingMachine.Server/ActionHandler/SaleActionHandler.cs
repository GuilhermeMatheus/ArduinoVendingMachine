using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Services;
using VendingMachine.Infrastructure.Actions;
using VendingMachine.Infrastructure.Sale;

namespace VendingMachine.Infrastructure.ActionHandler
{
    public enum SaleResult
    {
        Success = (1<<7),
        InsufficientFunds = (1 <<6),
        InvalidPrice = (1 <<5),
        UserNotFound = (1 <<4),
        ProductNotFound = (1 <<3)
    }

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

            if (saleResult.Succeeded)
            {

            }

            return new byte[] { 0x00, 0x02, (byte)SaleResult.Success, 0x00, 0x00, 0xF0, 0x42 };
            throw new NotImplementedException();
        }
    }
}