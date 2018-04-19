using Microsoft.Extensions.Logging;
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
        private readonly IPAddress _IPAddress;

        private TcpListener _serverSocket;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        public TcpRequestListener(
            IActionContextProvider contextProvider,
            IActionHandlerProvider actionHandlerProvider,
            ILoggerFactory loggerFactory,
            ILogger<RequestListenerBase> baseLogger
            )
            :base(
                 contextProvider,
                 actionHandlerProvider,
                 (loggerFactory ?? throw new ArgumentNullException(nameof(loggerFactory))).CreateLogger<RequestListenerBase>()
            )
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.CreateLogger<TcpRequestListener>();
            
            //TODO: Remove ConfigurationManager from .NET Framework style
            var tcpPort = ConfigurationManager.AppSettings["tcp:port"];
            var ipAddr = ConfigurationManager.AppSettings["ip:addr"];
            
            _tcpPort = Int32.TryParse(tcpPort, out int port)
                ? port
                : 4444;
            
            _IPAddress = IPAddress.TryParse(ipAddr, out IPAddress addr)
                ? addr
                : IPAddress.Any;
        }

        public override void Start()
        {
            _logger.LogInformation("Starting server socket.");
            _logger.LogInformation($"IP:Address is {_IPAddress}");
            _logger.LogInformation($"TCP:Port is {_tcpPort}");

            _serverSocket = new TcpListener(_IPAddress, _tcpPort);
            _serverSocket.Start();
        }

        protected override async Task<IRequestHandler> GetRequestHandler()
        {
            var clientSocket = await _serverSocket.AcceptTcpClientAsync();            
            return new TcpRequestHandler(clientSocket, _loggerFactory.CreateLogger<TcpRequestHandler>());
        }

        public override void Dispose()
        {
            _serverSocket.Stop();
        }
    }
}
