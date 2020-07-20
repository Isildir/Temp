using NLog;
using System;

namespace EngineerProject.API.Utility
{
    public static class Logger
    {
        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public static void Log(string target, string message, LogLevel logLevel, Exception exception = null)
                    => logger.Log(logLevel, exception, $"{target} => {message}");
    }
}