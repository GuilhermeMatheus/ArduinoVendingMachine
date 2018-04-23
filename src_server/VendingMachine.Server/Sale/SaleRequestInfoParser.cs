using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Operations;
using static VendingMachine.Infrastructure.Helpers.ByteHelper;

namespace VendingMachine.Infrastructure.Sale
{
    /*
     * Toda implementação desta classe segue a especificação em
     * ArduinoVendingMachine/docs/Protocolo/Venda.md
     * 
     * Não altere detalhes do protocolo sem antes atualizar a especificação.
     */
    internal static class SaleRequestInfoParser
    {
        public static SaleRequestInfo Parse(ReadOnlySpan<byte> bytes)
        {
            var vendingMachineId = GetMachineId(bytes, 1);
            var clientCardId = GetMifareId(bytes, 3);
            var itemsCount = bytes[7];
            
            var priceBytes = bytes.Slice(8 + itemsCount, 4).ToArray();
            var itemsId = bytes.Slice(8, itemsCount).ToArray();
            
            var price = BitConverter.ToSingle(priceBytes, 0);

            return new SaleRequestInfo(
                machineId: vendingMachineId,
                clientCardId: clientCardId,
                itemsCount: itemsCount,
                itemsId: itemsId,
                price: price);
        }
    }
}
