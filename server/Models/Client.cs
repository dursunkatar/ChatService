using System.IO;
using System.Net.Sockets;

namespace server.Models
{
    public class Client
    {
        public bool IsConnected { get; set; } = true;
        public bool IsMessageLimitExpired { get; set; }
        public TcpClient TcpClient { get; private set; }
        public StreamWriter StreamWriter { get; private set; }
        public StreamReader StreamReader { get; private set; }
        public NetworkStream NetworkStream { get; private set; }

        public Client(TcpClient tcpClient)
        {
            TcpClient = tcpClient;
            NetworkStream = tcpClient.GetStream();
            StreamWriter = new StreamWriter(NetworkStream);
            StreamReader = new StreamReader(NetworkStream);
        }
    }
}
