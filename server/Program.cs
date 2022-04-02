using System;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ListenSocket.Start(1453);

            Console.ReadKey(true);
        }
    }
}

