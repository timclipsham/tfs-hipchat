namespace TfsHipChat
{
    public interface ILogger
    {
        void Log(LogLevel level, string message);
    }

    public enum LogLevel
    {
        Trace,
        Info,
        Warn,
        Error
    }

    public static class LoggerExtensions
    {
        public static void Trace(this ILogger logger, string format, params object[] args)
        {
            logger.Log(LogLevel.Trace, FormatMessage(format, args));
        }

        public static void Info(this ILogger logger, string format, params object[] args)
        {
            logger.Log(LogLevel.Info, FormatMessage(format, args));
        }

        public static void Warn(this ILogger logger, string format, params object[] args)
        {
            logger.Log(LogLevel.Warn, FormatMessage(format, args));
        }

        public static void Error(this ILogger logger, string format, params object[] args)
        {
            logger.Log(LogLevel.Error, FormatMessage(format, args));
        }

        private static string FormatMessage(string format, object[] args)
        {
            return args == null || args.Length == 0 
                ? format 
                : string.Format(format, args);
        }
    }
}
