using System;
using server.Models;
using server.Managers;
using System.Net.Sockets;
using System.Collections.Generic;

namespace server.Lists
{
    internal static class ClientList
    {
        private static volatile object obj = new object();
        private static readonly List<Client> clients;
        static ClientList()
        {
            clients = new();
        }
        public static ClientManager Add(TcpClient tcpClient)
        {
            var client = new Client(tcpClient);
            lock (obj)
            {
                clients.Add(client);
            }
            return new ClientManager(client);
        }

        public static void ForEach(Client currentClient, Action<Client> callback)
        {
            lock (obj)
            {
                foreach (var client in clients)
                {
                    if (client != currentClient && client.IsConnected)
                    {
                        callback(client);
                    }
                }
            }
        }
    }
}
