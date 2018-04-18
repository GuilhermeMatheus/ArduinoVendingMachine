using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Core.Operations;
using static VendingMachine.Server.Helpers.ByteHelper;

namespace VendingMachine.Server.Sale
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
            
            var bytesThatWeCannotUseAsSpan = bytes.Slice(8).ToArray();
            var itemsId = bytesThatWeCannotUseAsSpan.Take(itemsCount).ToArray();
            
            var price = BitConverter.ToSingle(bytesThatWeCannotUseAsSpan, itemsCount);

            return new SaleRequestInfo(
                machineId: vendingMachineId,
                clientCardId: clientCardId,
                itemsCount: itemsCount,
                itemsId: itemsId,
                price: price);
        }
    }
}
