using server;
using server.Lists;
using server.Loggers;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class ServerTest
    {
        [Fact]
        public void TcpListenManager_Start()
        {
            #region Arrange
            TcpListenManager listener = new();
            #endregion
            #region Assert
            try
            {
                listener.Start(1991);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
            #endregion
        }

        [Fact]
        public async Task ServerClientManager_StartReceiveMessageAsync()
        {
            #region Arrange
            TcpListenManager listener = new();
            TcpClient _tcpClient = new();
            #endregion
            #region Act
            listener.Start(1992);
            await _tcpClient.ConnectAsync("127.0.0.1", 1992);
            var clientManager = ClientList.Add(_tcpClient);
            #endregion
            #region Assert
            try
            {
                _ = clientManager.StartReceiveMessageAsync(new List<ILogger>());
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
            #endregion
        }
    }
}
