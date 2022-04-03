using server.Loggers;
using System;

namespace server
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpListenManager listener = new();

            listener.AddLogger(new FileLogger())
                    .AddLogger(new FtpLogger())
                    .Start(1453);

            Console.WriteLine("1453 numaralı port dinleniyor...");

            Console.ReadKey(true);
        }
    }
}

