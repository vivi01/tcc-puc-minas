using GISA.EventBusRabbitMQ.Settings;
using Microsoft.Extensions.ObjectPool;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace GISA.EventBusRabbitMQ
{
    public class RabbitModelPooledObjectPolicy : IPooledObjectPolicy<IModel>
    {
        private readonly RabbitOptionsSettings _options;

        private readonly IConnection _connection;

        public RabbitModelPooledObjectPolicy(IOptions<RabbitOptionsSettings> optionsAccs)
        {
            _options = optionsAccs.Value;
            _connection = GetConnection();
        }

        private IConnection GetConnection()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _options.HostName             
            };

            return factory.CreateConnection();
        }

        public IModel Create()
        {
            return _connection.CreateModel();
        }

        public bool Return(IModel obj)
        {
            if (obj.IsOpen)
            {
                return true;
            }
            else
            {
                obj?.Dispose();
                return false;
            }
        }
    }
}
