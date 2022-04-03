using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace server
{
    internal class ListenSocket
    {
        public static void Start(int port)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, port);
            var server = new TcpListener(endpoint);
            server.Start();
            _ = acceptTcpClientAsync(server);
        }
        private static async Task acceptTcpClientAsync(TcpListener server)
        {
            do
            {
                var tcpClient = await server.AcceptTcpClientAsync();

                var clientSocket = ClientList.Add(tcpClient);

                _ = clientSocket.StartReceiveMessageAsync();

            } while (true);

        }

    }
}
