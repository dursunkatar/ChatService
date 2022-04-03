using System;
namespace server.Loggers
{
    internal class FileLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine("File Log: " + message);
        }
    }
}
