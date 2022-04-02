using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace server
{
    internal class SocketListen
    {
        public static void Listen(int port)
        {
            var endpoint = new IPEndPoint(IPAddress.Parse("10.48.48.50"), port);
            var server = new TcpListener(endpoint);
            server.Start();
            _ = AcceptTcpClientAsync(server);
        }
        private static async Task AcceptTcpClientAsync(TcpListener server)
        {
            for (; ; )
            {
                var client = await server.AcceptTcpClientAsync();

                string clientId = ClientList.AddClient(client);

                var clientSocket = new ClientSocket(clientId, client);
                clientSocket.StartReceiveMessage();
            }
        }

    }
}
