using System;

namespace TfsHipChat
{
    public static class LoggerInstance
    {
        static LoggerInstance()
        {
            Current = new BasicConsoleLogger();
        }

        public static ILogger Current { get; set; }
    }

    public class BasicConsoleLogger : ILogger
    {
        public void Log(LogLevel level, string message)
        {
            Console.WriteLine("[{0}] {1}", level, message);
        }
    }
}
