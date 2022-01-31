using TaskAlfa.Data.Models;
using Microsoft.Extensions.Logging;
using System;


namespace TaskAlfa.Data.SharedServices
{
    public class Logger : ILogger
    {
        private object _lock = new object();
        private IPushNotificationsQueue _pr { get; }
        public Logger(IPushNotificationsQueue pr)
        {
            _pr = pr;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel > LogLevel.Information;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (formatter != null && IsEnabled(logLevel))
            {
                lock (_lock)
                {
                    var login_state = state.ToString().Split('*');
                    LogMessageEntry message = new LogMessageEntry()
                    {
                        ErrorMsg = (login_state.Length > 1 ? login_state[1] + Environment.NewLine : "") + exception?.Message,
                        UserData = login_state.Length > 1 ? login_state[0] : "Unknown",//_httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "Unknown",
                        ErrorLevel = (int)logLevel,
                        ErrorContext = exception?.StackTrace ?? "",
                        BrowserInfo = login_state.Length > 2 ? login_state[2] : "",
                        AppVersion = Globals.AppVersion
                    };
                    if (string.IsNullOrEmpty(message.ErrorMsg)) message.ErrorMsg = formatter(state, exception);
                    _pr.Enqueue(message);
                    Console.WriteLine(formatter(state, exception) + Environment.NewLine);
                }
            }
        }
    }
}
