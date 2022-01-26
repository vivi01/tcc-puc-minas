using GISA.EventBusRabbitMQ.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GISA.EventBusRabbitMQ.Extensions
{
    public static class RabbitExtension
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connection)
        {
            if (string.IsNullOrEmpty(connection)) throw new ArgumentNullException();

            services.AddSingleton<IMessageBus>(new MessageBus(connection));

            return services;
        }
    }
}
