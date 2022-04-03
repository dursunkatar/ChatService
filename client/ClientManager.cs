using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace client
{
    internal class ClientManager : IDisposable
    {

        private NetworkStream _networkStream;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private readonly TcpClient _tcpClient;

        public ClientManager()
        {
            _tcpClient = new();
        }

        public async Task ConnectAsyc(string host, int port)
        {
            await _tcpClient.ConnectAsync(host, port);
            initStreams();
        }

        private void initStreams()
        {
            _networkStream = _tcpClient.GetStream();
            _streamWriter = new StreamWriter(_networkStream);
            _streamReader = new StreamReader(_networkStream);
        }

        public async Task StartReceiveMessageAsync(Action<string> callback)
        {
            do
            {
                var message = await _streamReader.ReadLineAsync();
                callback(message);
            } while (_networkStream.CanRead);
        }

        public async Task SendMessageAsync(string message)
        {
            await _streamWriter.WriteLineAsync(message);
            await _streamWriter.FlushAsync();
        }

        public void Dispose()
        {
            _tcpClient.Dispose();
        }
    }
}
