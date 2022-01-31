using AlfaControlingDb;
using AlfaControlingDb.Models;
using DbRepository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Diagnostics;
using System.Threading;
using TaskAlfa.Data.Models;

namespace TaskAlfa.Data.SharedServices
{
    public class PushNotificationsDequeuer : IHostedService, IDisposable
    {
        private Timer _timer;
        private System.Threading.Tasks.Task _executingTask;
        private readonly IPushNotificationsQueue _messagesQueue;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private EFRepository<LogApplicationError> appRepo { get; set; }
        private readonly IServiceScopeFactory _scopeFactory;

        public PushNotificationsDequeuer(IPushNotificationsQueue messagesQueue, IServiceScopeFactory scopeFactory)
        {
            _messagesQueue = messagesQueue;
            _scopeFactory = scopeFactory;
        }

        public System.Threading.Tasks.Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(ExecuteTask, null, TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(-1));

            return System.Threading.Tasks.Task.CompletedTask;
        }

        private void ExecuteTask(object state)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _executingTask = ExecuteTaskAsync(_stoppingCts.Token);
        }

        private async System.Threading.Tasks.Task DequeueMessagesAsync(CancellationToken stoppingToken)
        {
            LogMessageEntry message;
            do
            {
                message = await _messagesQueue.DequeueAsync(stoppingToken);

                if (!stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        using (var scope = _scopeFactory.CreateScope())
                        {
                            var _context = scope.ServiceProvider.GetRequiredService<ControllingDbContext>();
                            appRepo = new EFRepository<LogApplicationError>(_context);
                            appRepo.Create(new LogApplicationError()
                            {
                                InsertDate = DateTime.Now,
                                ErrorMsg = message.ErrorMsg,
                                ErrorLevel = message.ErrorLevel,
                                UserData = message.UserData,
                                ErrorContext = message.ErrorContext,
                                BrowserInfo = message.BrowserInfo,
                                AppVersion = message.AppVersion
                            });
                        }
                    }
                    catch (Exception e)
                    {
                        if (!EventLog.SourceExists(ConstField.LogSource))
                            EventLog.CreateEventSource(ConstField.LogSource, ConstField.LogName);
                        EventLog.WriteEntry(ConstField.LogSource, $"Exception - {e.Message}", EventLogEntryType.Error);
                    }
                }
            } while (message != null);
        }

        private async System.Threading.Tasks.Task ExecuteTaskAsync(CancellationToken stoppingToken)
        {
            await DequeueMessagesAsync(stoppingToken);
            _timer.Change(TimeSpan.FromSeconds(10), TimeSpan.FromMilliseconds(-1));
        }

        public virtual async System.Threading.Tasks.Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);

            if (_executingTask == null)
            {
                return;
            }

            try
            {
                _stoppingCts.Cancel();
            }
            finally
            {
                await System.Threading.Tasks.Task.WhenAny(_executingTask, System.Threading.Tasks.Task.Delay(Timeout.Infinite, cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
            _timer?.Dispose();
        }
    }
}