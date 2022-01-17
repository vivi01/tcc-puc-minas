using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public readonly IPrestadorRepository _prestadorRepository;
        private readonly IPlanoService _planoService;
        private readonly IBus _busControl;

        public PrestadorService(IPrestadorRepository prestadorRepository, IBus busControl, IPlanoService planoService)
        {
            _prestadorRepository = prestadorRepository;
            _busControl = busControl;
            _planoService = planoService;
        }
        public async Task<string> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            if (autorizacaoExameMsg.StatusAssociado != "Ativo")
            {
                //await _busControl.ReceiveAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue,
                //     x =>
                //     {
                //         x.MensagensErro = "Associado não está ativo";
                //         Task.Run(() => { GetAutorizacao(x); });
                //     });

                return "Nao Autorizado! Associado não está ativo";
            }

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado == null)
            {
                return "Nao Autorizado! Plano não conveniado";
            }

            //chama o legado SGPS para solicitar a autorização do exame
            GetAutorizacaoExame(autorizacaoExameMsg);                     

            if (autorizacaoExameMsg.Status != "Autorizado")
            {
                return "Nao Autorizado";
            }

            return "Autorizado";
        }

        private async void GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue, requestMessage);
        }

        public async Task<bool> CadastrarPrestador(Entities.Prestador prestador)
        {
            return await _prestadorRepository.Add(prestador);
        }

        public async Task<Plano> GetPlanoConveniado(int codigoPlano)
        {
            return await _planoService.ObterPlanoPorCodigo(codigoPlano);
        }

        public async Task<List<Plano>> GetAllPlanosConveniados()
        {
            return await _planoService.ObterTodos();
        }
    }
}
