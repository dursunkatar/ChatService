using System.Threading.Tasks;

namespace server
{
    internal class ClientManager
    {
        private readonly Client _client;

        public ClientManager(Client client)
        {
            _client = client;
        }

        public async Task StartReceiveMessageAsync()
        {
            do
            {
                var message = await _client.StreamReader.ReadLineAsync();
                SendMessageAllClients(message);

            } while (_client.NetworkStream.CanRead);
        }

        private void SendMessageAllClients(string message)
        {
            ClientList.ForEach(_client, async client =>
            {
                try
                {
                    await client.StreamWriter.WriteLineAsync(message);
                    await client.StreamWriter.FlushAsync();
                }
                catch { client.IsConnected = false; }
            });
        }
    }
}
