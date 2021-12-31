using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using GISA.EventBusRabbitMQ;

namespace GISA.Associado.Extensions
{
    public static class ApplicationBuilderExtentions
    {
        public static RabbitBus Listener { get; set; }

        public static IApplicationBuilder UseRabbitListener(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<RabbitBus>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life?.ApplicationStarted.Register(OnStarted);           

            return app;
        }

        private static void OnStarted()
        {
            
        }        
    }
}
