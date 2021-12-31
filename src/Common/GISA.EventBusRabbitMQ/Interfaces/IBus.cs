using System;
using System.Threading.Tasks;

namespace GISA.EventBusRabbitMQ.Interfaces
{
    public interface IBus
    {
        Task SendAsync<T>(string exchange, T message);
        Task ReceiveAsync<T>(string exchange, string queue, Action<T> onMessage);
    }
}
