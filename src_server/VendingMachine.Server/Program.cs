using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Server.Actions;

namespace VendingMachine.Server
{
    class Program
    {
        // private static readonly int TCP_PORT = Int32.Parse(ConfigurationManager.AppSettings["tcp:port"]);

        static void Main(string[] args)
        {
            //var serverSocket = new TcpListener(IPAddress.Any, TCP_PORT);
            var serverSocket = new TcpListener(IPAddress.Any, 4444);
            
            serverSocket.Start();
            
            while (true)
            {
                try
                {
                    var clientSocket = serverSocket.AcceptTcpClient();
                    var networkStream = clientSocket.GetStream();
                    var bytesFrom = new byte[20];

                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);
                    var actionType = (ActionType)bytesFrom[0];

                    var dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    Console.WriteLine(" >> Data from client - " + dataFromClient);
                    string serverResponse = "Last Message from client" + dataFromClient;
                    var sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            serverSocket.Stop();
            Console.WriteLine(" >> exit");
            Console.ReadLine();
        }
    }
}
