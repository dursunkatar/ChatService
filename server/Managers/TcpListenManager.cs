using server.Loggers;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace server
{
    internal class TcpListenManager
    {
        private readonly List<ILogger> loggers;

        public TcpListenManager()
        {
            loggers = new();
        }
        public TcpListenManager AddLogger(ILogger logger)
        {
            loggers.Add(logger);
            return this;
        }
        public void Start(int port)
        {
            var endpoint = new IPEndPoint(IPAddress.Any, port);
            var server = new TcpListener(endpoint);
            server.Start();
            _ = acceptTcpClientAsync(server);
        }
        private async Task acceptTcpClientAsync(TcpListener server)
        {
            do
            {
                var tcpClient = await server.AcceptTcpClientAsync();

                var clientManager = ClientList.Add(tcpClient);

                _ = clientManager.StartReceiveMessageAsync(loggers);

            } while (true);

        }
    }
}
