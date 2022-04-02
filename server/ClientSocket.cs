using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace server
{
    internal class ClientSocket
    {
        private readonly string _clientId;
        private readonly TcpClient _tcpClient;

        public ClientSocket(string clientId, TcpClient tcpClient)
        {
            _clientId = clientId;
            _tcpClient = tcpClient;
        }

        public void StartReceiveMessage()
        {
            var networkStream = _tcpClient.GetStream();
            var streamReader = new StreamReader(networkStream, Encoding.UTF8);

            Task task = new Task(async () =>
            {
                while (networkStream.CanRead)
                {
                    if (_tcpClient.Client.Available > 0)
                    {
                        var message = await streamReader.ReadLineAsync();
                        Console.WriteLine(message);
                        await ClientList.SendMessageAllClientsAsync(_clientId, message);
                    }
                }
            });
            task.Start();
        }

    }
}
