using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace server
{
    internal static class ClientList
    {
        private static volatile object obj = new object();
        private static readonly Dictionary<string, StreamWriter> clients;
        static ClientList()
        {
            clients = new();
        }

        /// <summary>
        /// Gelen Bağlantıyı Listeye Ekler
        /// </summary>
        /// <param name="client"></param>
        public static string AddClient(TcpClient client)
        {
            string clientId = Utility.RandomClientId();
            lock (obj)
            {
                clients.Add(clientId, new StreamWriter(client.GetStream()));
            }
            return clientId;
        }



        /// <summary>
        /// Bütün Client'lere gelen mesajı iletir
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task SendMessageAllClientsAsync(string clientId, string message)
        {
            foreach (var client in clients)
            {
                if (client.Key != clientId)
                {
                    await client.Value.WriteLineAsync(message);
                    await client.Value.FlushAsync();
                }
            }
        }
    }
}
