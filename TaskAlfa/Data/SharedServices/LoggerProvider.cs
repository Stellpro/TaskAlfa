
using Microsoft.Extensions.Logging;
using TaskAlfa.Data.SharedServices;

namespace TaskAlfa.SharedServices
{
    public class LoggerProvider : ILoggerProvider
    {
        private IPushNotificationsQueue _pr { get; }
        public LoggerProvider(IPushNotificationsQueue pr)
        {
            _pr = pr;
        }
        public ILogger CreateLogger(string categoryName) => new Logger(_pr);

        public void Dispose()
        {
        }
    }
}
