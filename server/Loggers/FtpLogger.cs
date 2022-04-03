using System;
namespace server.Loggers
{
    internal class FtpLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("FTP Log: " + message);
        }
    }
}
