using System;
using System.Threading.Tasks;

namespace client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ClientManager clientManager = new();

            try
            {
                await clientManager.ConnectAsyc("127.0.0.1", 1453);
                Console.WriteLine("Bağlandı =)" + Environment.NewLine);

                _ = clientManager.StartReceiveMessageAsync(msg => Console.WriteLine("Gelen: " + msg));

                do
                {
                    string message = Console.ReadLine();
                    await clientManager.SendMessageAsync(message);

                } while (true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                clientManager.Dispose();
            }

        }
    }
}
