using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace VendingMachine.Core.Helpers
{
    public static class LoggerHelper
    {
        public static void LogInformationIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Information))
                logger.LogInformation(func());
        }

        public static void LogTraceIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Trace))
                logger.LogTrace(func());
        }

        public static void LogDebugIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Debug))
                logger.LogDebug(func());
        }

        public static void LogWarningIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Warning))
                logger.LogWarning(func());
        }

        public static void LogErrorIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Error))
                logger.LogError(func());
        }

        public static void LogCriticalIfEnabled(this ILogger logger, Func<string> func)
        {
            if (logger.IsEnabled(LogLevel.Critical))
                logger.LogCritical(func());
        }
    }
}
