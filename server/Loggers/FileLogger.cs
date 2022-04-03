using System;
namespace server.Loggers
{
    public class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("File Log: " + message);
        }
    }
}
