using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Model;
using VendingMachine.Core.Operations;
using VendingMachine.Core.Repository;
using VendingMachine.Core.Services;
using VendingMachine.Infrastructure.Actions;
using VendingMachine.Infrastructure.Sale;

namespace VendingMachine.Infrastructure.ActionHandler
{
    public enum SaleResult
    {
        Success = (1 << 7),
        InsufficientFunds = (1 << 6),
        InvalidPrice = (1 << 5),
        UserNotFound = (1 << 4),
        ProductNotFound = (1 << 3)
    }

    public class SaleActionHandler : IActionHandler
    {
        private readonly ISaleService _saleService;
        private readonly IClientCardRepository _clientCardRepository;

        public SaleActionHandler(ISaleService saleService, IClientCardRepository clientCardRepository)
        {
            _saleService = saleService ?? throw new ArgumentNullException(nameof(saleService));
            _clientCardRepository = clientCardRepository ?? throw new ArgumentNullException(nameof(clientCardRepository));
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
            var card = _clientCardRepository.Get(sri.ClientCardId);

            return BuildResponse(saleResult, card);
        }

        private byte[] BuildResponse(OperationResult saleResult, ClientCard card)
        {
            var result = new byte[7];
            var priceBytes = BitConverter.GetBytes(Convert.ToSingle(card?.Credit));

            result[0] = 0x00; // transaction id MSB
            result[1] = 0x02; // transaction id LSB
            result[2] = GetSaleResult(saleResult);

            result[3] = priceBytes[0];
            result[4] = priceBytes[1];
            result[5] = priceBytes[2];
            result[6] = priceBytes[3];

            return result;
        }

        private byte GetSaleResult(OperationResult operationResult)
        {
            if (operationResult.Succeeded)
                return (byte)SaleResult.Success;

            byte result = 0;

            foreach (var item in operationResult.Errors)
                switch (item.Code)
                {
                    case (int)WellKnowErrors.ClientNotFound:
                        result |= (byte)SaleResult.UserNotFound;
                        break;
                    case (int)WellKnowErrors.ClientWithNoEnoughCredit:
                        result |= (byte)SaleResult.InsufficientFunds;
                        break;
                    case (int)WellKnowErrors.InvalidPrice:
                        result |= (byte)SaleResult.InvalidPrice;
                        break;
                    case (int)WellKnowErrors.InvalidProduct:
                        result |= (byte)SaleResult.ProductNotFound;
                        break;
                    default:
                        break;
                }

            return result;
        }
    }
}