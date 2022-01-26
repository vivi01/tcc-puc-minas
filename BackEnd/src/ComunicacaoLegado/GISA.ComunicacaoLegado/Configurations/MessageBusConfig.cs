using GISA.ComunicacaoLegado.RabbitmqConsumer;
using GISA.EventBusRabbitMQ.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GISA.ComunicacaoLegado.Configurations
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
           IConfiguration configuration)
        {
            var connection = configuration["MessageBus"]; 

            services.AddMessageBus(connection)
                .AddHostedService<AutorizacaoExameConsumer>();
        }
    }
}
