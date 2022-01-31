using TaskAlfa.Data.Models;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAlfa.Data.SharedServices
{
    public interface IPushNotificationsQueue
    {
        void Enqueue(LogMessageEntry message);

        Task<LogMessageEntry> DequeueAsync(CancellationToken cancellationToken);
    }
}