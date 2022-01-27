using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using GISA.Prestador.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.Prestador.RabbitmqConsumer
{
    public class MarcacaoExameConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMessageBus _bus;
        private readonly IPrestadorService _prestadorService;
        public MarcacaoExameConsumer(IPrestadorService prestadorService, IServiceProvider serviceProvider, IMessageBus bus)
        {
            _prestadorService = prestadorService;
            _serviceProvider = serviceProvider;
            _bus = bus;
        }
        private void SetResponder()
        {
            _bus.RespondAsync<MarcacaoExameMsg, AutorizacaoExameResponse>(async request =>
            await MarcarExame(request));

            _bus.AdvancedBus.Connected += OnConnect;
        }
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }
        private void OnConnect(object s, EventArgs e) => SetResponder();
        private async Task<AutorizacaoExameResponse> MarcarExame(MarcacaoExameMsg marcacaoExameRequest)
        {
            AutorizacaoExameResponse result = null;
            using (var scope = _serviceProvider.CreateScope())
            {
                result = await _prestadorService.SolicitarAutorizacaoExame(marcacaoExameRequest);
            }

            return result;
        }
    }
}
