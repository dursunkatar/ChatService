using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            TcpClient tcpClient = new TcpClient("10.48.48.50", 991);

            var clientSocketManager = new ClientSocketManager(tcpClient);

            clientSocketManager.StartReceiveMessage();

            for (; ; )
            {
                string message = Console.ReadLine();
                await clientSocketManager.SendMessageAsync(message);
            }
        }
    }
}
