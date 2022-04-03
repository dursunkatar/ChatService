using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;

namespace server
{
    internal static class ClientList
    {
        private static volatile object obj = new object();
        private static readonly List<Client> clients;
        static ClientList()
        {
            clients = new();
        }
        public static ClientSocket Add(TcpClient tcpClient)
        {
            string clientId = Utility.RandomClientId();
            lock (obj)
            {
                clients.Add(new Client(clientId, new StreamWriter(tcpClient.GetStream())));
            }
            return new ClientSocket(clientId, tcpClient);
        }

        public static void DisableClient(Client client)
        {
            lock (obj)
            {
                client.IsConnected = false;
            }
        }

        public static void ForEach(string clientId, Action<Client> callback)
        {
            lock (obj)
            {
                foreach (var client in clients)
                {
                    if (client.ClientId != clientId && client.IsConnected)
                    {
                        callback(client);
                    }
                }
            }
        }
    }
}
