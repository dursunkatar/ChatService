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

        public async Task StartReceiveMessageAsync()
        {
            var networkStream = _tcpClient.GetStream();
            var streamReader = new StreamReader(networkStream, Encoding.UTF8);

            do
            {
                var message = await streamReader.ReadLineAsync();
                SendMessageAllClients(message);

            } while (networkStream.CanRead);
        }

        private void SendMessageAllClients(string message)
        {
            ClientList.ForEach(_clientId, async client =>
            {
                try
                {
                    await client.SendMessageAsync(message);
                }
                catch { ClientList.DisableClient(client); }
            });
        }
    }
}
