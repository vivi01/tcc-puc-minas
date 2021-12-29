using GISA.EventBusRabbitMQ.Events;
using Newtonsoft.Json;
using System;
using System.Text;

namespace GISA.EventBusRabbitMQ.Producer
{
    public class EventBusRabbitMqProducer
    {
        //TODO analizar se deve mover isso para cada um dos produtores.
        private readonly IRabbitMqConnection _connection;

        public EventBusRabbitMqProducer(IRabbitMqConnection connection)
        {
            _connection = connection;
        }

        public void Publish(string queueName, CheckoutEvent publishModel)
        {
            using (var channel = _connection.CreateModel())
            {
                channel.QueueDeclare(queueName, false, false, false, null);
                var message = JsonConvert.SerializeObject(publishModel);
                var body = Encoding.UTF8.GetBytes(message);

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                properties.DeliveryMode = 2;

                channel.ConfirmSelect();
                channel.BasicPublish("", queueName, true, properties, body);
                channel.WaitForConfirmsOrDie();

                channel.BasicAcks += (sender, eventArgs) =>
                {
                    Console.WriteLine($"Sent RabbitMQ { queueName }");
                };

                channel.ConfirmSelect();
            }
        }
    }
}
