using System;
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
        private TcpClient _clientSocket;
        private byte[] _bytesFrom;
                
        public TcpRequestHandler(TcpClient clientSocket)
        {
            _clientSocket = clientSocket ?? throw new ArgumentNullException(nameof(clientSocket));
            SetNetworkStream(clientSocket.GetStream());
        }

        public RequestData GetRequestData()
        {
            var result = new RequestData(_bytesFrom);
            result["machine:ip"] = _clientSocket.Client.RemoteEndPoint;

            return result;
        }

        public async Task SendResponse(byte[] data)
        {
            _networkStream.Write(data, 0, data.Length);
            await _networkStream.FlushAsync();

            _clientSocket.Close();
        }

        private void SetNetworkStream(NetworkStream networkStream)
        {
            _networkStream = networkStream;
            
            _bytesFrom = new byte[20];
            networkStream.Read(_bytesFrom, 0, _bytesFrom.Length);
        }
    }
}
