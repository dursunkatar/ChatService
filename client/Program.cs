using System;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ClientSocket clientSocket = new();

            try
            {
                await clientSocket.ConnectAsyc("127.0.0.1", 1453);
                Console.WriteLine("Connected :)");

                _ = clientSocket.StartReceiveMessageAsync(msg => Console.WriteLine("Received: " + msg));

                do
                {
                    string message = Console.ReadLine();
                    await clientSocket.SendMessageAsync(message);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                clientSocket.Dispose();
            }

        }
    }
}
