using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server.Request
{
    public sealed class TcpRequestListener : RequestListenerBase
    {
        private readonly int _tcpPort;
        private TcpListener _serverSocket;

        public TcpRequestListener(IActionContextProvider contextProvider, IActionHandlerProvider actionHandlerProvider)
            :base(contextProvider, actionHandlerProvider)
        {
            var config = ConfigurationManager.AppSettings["tcp:port"];

            _tcpPort = Int32.TryParse(config, out int port)
                ? port
                : 4444;
        }

        public override void Start()
        {
            _serverSocket = new TcpListener(IPAddress.Any, _tcpPort);
            _serverSocket.Start();
        }

        protected override async Task<IRequestHandler> GetRequestHandler()
        {
            var clientSocket = await _serverSocket.AcceptTcpClientAsync();            
            return new TcpRequestHandler(clientSocket);
        }

        public override void Dispose()
        {
            _serverSocket.Stop();
        }
    }
}
