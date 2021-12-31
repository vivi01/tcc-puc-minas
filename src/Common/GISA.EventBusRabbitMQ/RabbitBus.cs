using GISA.EventBusRabbitMQ.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading.Tasks;

namespace GISA.EventBusRabbitMQ
{
    public class RabbitBus : IBus
    {
        private readonly IModel _channel;
        internal RabbitBus(IModel channel)
        {
            _channel = channel;
        }

        public async Task SendAsync<T>(string exchange, T message)
        {
            await Task.Run(() =>
            {
                _channel.ConfirmSelect();

                _channel.ExchangeDeclare(exchange, ExchangeType.Direct, true, false, null);

                var properties = _channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                var output = JsonConvert.SerializeObject(message);

                _channel.BasicPublish(exchange, "account.info", null, Encoding.UTF8.GetBytes(output));
            });
        }
        public async Task ReceiveAsync<T>(string exchange, string queue, Action<T> onMessage)
        {
            _channel.ExchangeDeclare(exchange, ExchangeType.Direct, true, false, null);
            _channel.QueueDeclare(queue, true, false, false);
            _channel.QueueBind(queue, exchange, "account.info");

            var consumer = new AsyncEventingBasicConsumer(_channel);

            consumer.Received += async (s, e) =>
            {
                var jsonSpecified = Encoding.UTF8.GetString(e.Body.Span);
                var item = JsonConvert.DeserializeObject<T>(jsonSpecified);
                onMessage(item);
                await Task.Yield();
            };
            _channel.BasicConsume(queue, true, consumer);
            await Task.Yield();
        }      
    }
}
