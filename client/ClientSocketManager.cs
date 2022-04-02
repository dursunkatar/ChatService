using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace client
{
    internal class ClientSocketManager
    {
        private readonly TcpClient _tcpClient;
        private readonly NetworkStream _networkStream;
        private readonly StreamWriter _streamWriter;
        private readonly StreamReader _streamReader;

        public ClientSocketManager(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _networkStream = _tcpClient.GetStream();
            _streamWriter = new StreamWriter(_networkStream);
            _streamReader = new StreamReader(_networkStream);
        }

        public void StartReceiveMessage()
        {
            Task task = new Task(async () =>
            {
                while (_networkStream.CanRead)
                {
                    if (_tcpClient.Client.Available > 0)
                    {
                        var message = await _streamReader.ReadLineAsync();
                        Console.WriteLine("Received: " + message);
                    }
                }
            });

            task.Start();
        }

        public async Task SendMessageAsync(string message)
        {
            await _streamWriter.WriteLineAsync(message);
            await _streamWriter.FlushAsync();
        }
    }
}
