using System.IO;
using System.Threading.Tasks;

namespace server
{
    internal class Client
    {
        public string ClientId { get; private set; }
        public StreamWriter Writer { get; private set; }
        public bool IsConnected { get; set; } = true;

        public Client(string clientId, StreamWriter writer)
        {
            ClientId = clientId;
            Writer = writer;
        }

        public async Task SendMessageAsync(string message)
        {
            await Writer.WriteLineAsync(message);
            await Writer.FlushAsync();
        }
    }
}
