using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;
using VendingMachine.Server.Exceptions;
using VendingMachine.Server.Request;

namespace VendingMachine.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            var contextProvider = new ActionContextProvider();
            var handlerProvider = new ActionHandlerProvider();
            var requestListener = new TcpRequestListener(contextProvider, handlerProvider);

            requestListener.Start();

            while (true)
            {
                try
                {
                    requestListener.Listen();
                }
                catch (ActionNotSupportedException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
