﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Server.Request
{
    public class TcpRequestHandler : IRequestHandler
    {
        private NetworkStream _networkStream;
        private readonly byte[] _bytesFrom;
        private TcpClient _clientSocket;
                
        public TcpRequestHandler(TcpClient clientSocket)
        {
            _clientSocket = clientSocket ?? throw new ArgumentNullException(nameof(clientSocket));
            SetNetworkStream(clientSocket.GetStream());
        }

        public byte[] GetRequestBytes() => _bytesFrom;

        public async Task SendResponse(byte[] data)
        {
            _networkStream.Write(data, 0, data.Length);
            await _networkStream.FlushAsync();

            _clientSocket.Close();
        }

        private void SetNetworkStream(NetworkStream networkStream)
        {
            _networkStream = networkStream;

            var bytesFrom = new byte[20];
            networkStream.Read(bytesFrom, 0, bytesFrom.Length);
        }
    }
}
