using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.RabbitmqConsumer
{
    public class AutorizacaoExameConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IServiceProvider _serviceProvider;
        public AutorizacaoExameConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);

            consumer.Received += (sender, eventArgs) =>
            {
                var byteArray = eventArgs.Body.ToArray();
                var autorizacaoInfoJson = Encoding.UTF8.GetString(byteArray);

                var autorizcaoInfo = JsonSerializer.Deserialize<AutorizacaoExameMsg>(autorizacaoInfoJson);

                GetAutorizacao(autorizcaoInfo);                

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(EventBusConstants.GisaQueue, false, consumer);

            return Task.CompletedTask;
        }

        private static void GetAutorizacao(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            autorizacaoExameMsg.Status = "Autorizado";
        }
    }
}
