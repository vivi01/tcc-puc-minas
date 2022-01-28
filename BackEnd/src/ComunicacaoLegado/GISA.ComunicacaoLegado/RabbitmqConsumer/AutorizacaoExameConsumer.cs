using GISA.ComunicacaoLegado.Services;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.RabbitmqConsumer
{
    public class AutorizacaoExameConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;
        private readonly ISgpsService _sgpsService;

        public AutorizacaoExameConsumer(IServiceProvider serviceProvider, IMessageBus bus, ISgpsService sgpsService)
        {
            _serviceProvider = serviceProvider;
            _bus = bus;
            _sgpsService = sgpsService;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<AutorizacaoExameMsg, AutorizacaoExameResponse>(async request =>
            await AutorizarExame(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object s, EventArgs e) => SetResponder();

        private async Task<AutorizacaoExameResponse> AutorizarExame(AutorizacaoExameMsg requestMessage)
        {
            AutorizacaoExameResponse result = null;
            using (var scope = _serviceProvider.CreateScope())
            {
                result = await _sgpsService.AutorizarExame(requestMessage);
            }

            return result;
        }
    }
}
