using System;
namespace server.Loggers
{
    public class FtpLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("FTP Log: " + message);
        }
    }
}
