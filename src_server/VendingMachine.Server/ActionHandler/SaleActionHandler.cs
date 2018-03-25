using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;
using VendingMachine.Server.Sale;

namespace VendingMachine.Server.ActionHandler
{
    /*
     * Toda implementação desta classe segue a especificação em
     * ArduinoVendingMachine/docs/Protocolo/Venda.md
     * 
     * Não altere detalhes do protocolo sem antes atualizar a especificação.
     */
    public class SaleActionHandler : IActionHandler
    {
        public byte[] Process(ActionContext context)
        {
            var bytes = context.IncomingMessage;

            var saleRequestInfo = SaleRequestInfoParser.Parse(bytes);
            
            throw new NotImplementedException();
        }
    }
}