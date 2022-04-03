using server.Lists;
using server.Loggers;
using server.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace server.Managers
{
    internal class ClientManager
    {
        private readonly Client _client;
        public ClientManager(Client client)
        {
            _client = client;
        }
        private void SendMessageAllClients(string message)
        {
            ClientList.ForEach(_client, async client => await SendMessage(client, message));
        }
        private async Task SendMessage(Client client, string message)
        {
            try
            {
                await client.StreamWriter.WriteLineAsync(message);
                await client.StreamWriter.FlushAsync();
            }
            catch
            {
                client.IsConnected = false;
                client.TcpClient.Dispose();
            }
        }
        public async Task StartReceiveMessageAsync(List<ILogger> loggers)
        {
            string lastReceviedMessageTime = string.Empty;
            do
            {
                var message = await _client.StreamReader.ReadLineAsync();
                string currentTime = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");

                bool ok = await checkLimitToSendMessage(lastReceviedMessageTime, currentTime);

                if (ok)
                {
                    SendMessageAllClients(message);
                    loggers.ForEach(logger => logger.Log(message));
                    lastReceviedMessageTime = currentTime;
                }

            } while (_client.NetworkStream.CanRead && _client.IsConnected);
        }
        private async Task<bool> checkLimitToSendMessage(string lastReceviedMessageTime, string currentTime)
        {
            if (lastReceviedMessageTime == currentTime && _client.IsMessageLimitExpired)
            {
                await SendMessage(_client, "[Server]: Bağlantınız Kapatılıyor...");
                _client.IsConnected = false;
                _client.TcpClient.Close();
                return false;
            }

            if (lastReceviedMessageTime == currentTime)
            {
                _client.IsMessageLimitExpired = true;
                await SendMessage(
                    _client
                    , @"[Uyarı]: 1 saniyede içinde 1 den fazla mesaj gönderemezsiniz,tekrarlamanız durumunda bağlantınız kapatılacak"
                    );
                return false;
            }

            return true;
        }
    }
}
