using client;
using server;
using System;
using System.Threading.Tasks;
using Xunit;

namespace UnitTest
{
    public class ClientTest
    {

        [Fact]
        public async Task ConnectAsyc()
        {
            #region Arrange
            ClientManager clientManager = new();
            TcpListenManager listener = new();
            listener.Start(1453);
            #endregion
            #region Assert
            try
            {
                await clientManager.ConnectAsyc("127.0.0.1", 1453);
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
            #endregion
        }

        [Fact]
        public async Task StartReceiveMessageAsync()
        {
            #region Arrange
            ClientManager clientManager = new();
            TcpListenManager listener = new();
            listener.Start(1454);
            #endregion
            #region Act
            await clientManager.ConnectAsyc("127.0.0.1", 1454);
            #endregion
            #region Assert
            try
            {
                _ = clientManager.StartReceiveMessageAsync(msg => Console.WriteLine("Gelen: " + msg));
                Assert.True(true);
            }
            catch
            {
                Assert.True(false);
            }
            #endregion
        }

        [Fact]
        public async Task SendMessageAsync()
        {
            #region Arrange
            ClientManager clientManager = new();
            TcpListenManager listener = new();
            listener.Start(1455);
            #endregion
            #region Act
            await clientManager.ConnectAsyc("127.0.0.1", 1455);
            #endregion
            #region Assert
            try
            {
                _ = clientManager.SendMessageAsync("test");
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
