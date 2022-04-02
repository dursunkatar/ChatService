using System;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SocketListen.Listen(991);

            Console.ReadKey(true);
        }
    }
}

