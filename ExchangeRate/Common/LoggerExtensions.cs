using Microsoft.Extensions.Logging;
using System.IO;
using System.Runtime.CompilerServices;

namespace ExchangeRate.Common
{
    static class LoggerExtensions
    {
        public static void LogInfoWithCaller(this ILogger logger, 
                                            string message,
                                            [CallerMemberName] string caller = "",
                                            [CallerFilePath] string file = "")
        {
            var className = Path.GetFileNameWithoutExtension(file);
            logger.LogInformation($"{className}.{caller} {message}");
        }
    }
}
