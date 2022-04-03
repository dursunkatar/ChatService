using System.IO;
using System.Net.Sockets;

namespace server
{
    internal class Client
    {
        public StreamWriter StreamWriter { get; private set; }
        public StreamReader StreamReader { get; private set; }
        public NetworkStream NetworkStream { get; private set; }
        public bool IsConnected { get; set; } = true;
        public Client(TcpClient tcpClient)
        {
            NetworkStream = tcpClient.GetStream();
            StreamWriter = new StreamWriter(NetworkStream);
            StreamReader = new StreamReader(NetworkStream);
        }
    }
}
