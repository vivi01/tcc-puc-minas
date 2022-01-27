using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public readonly IPrestadorRepository _prestadorRepository;
        private readonly IPlanoService _planoService;
        private readonly IMessageBus _bus;

        public PrestadorService(IPrestadorRepository prestadorRepository, IPlanoService planoService, IMessageBus bus)
        {
            _prestadorRepository = prestadorRepository;
            _planoService = planoService;
            _bus = bus;
        }
        public async Task<AutorizacaoExameResponse> SolicitarAutorizacaoExame(MarcacaoExameMsg marcacaoExameRequest)
        {
            if (marcacaoExameRequest.StatusAssociado != "Ativo")
            {
                return CriarResponseDefault("Nao Autorizado! Associado não está ativo", EStatusSolicitacao.NaoAutorizado);
            }

            var isConveniado = await GetPlanoConveniado(marcacaoExameRequest.CodigoPlano);

            if (isConveniado == null)
            {
                return CriarResponseDefault("Nao Autorizado! Plano não conveniado", EStatusSolicitacao.NaoAutorizado);
            }

            //chama o legado SGPS para solicitar a autorização do exame
            var marcacaoRequest = CriarAutorizacaoRequest(marcacaoExameRequest);
            return await GetAutorizacaoExame(marcacaoRequest);
        }

        private static AutorizacaoExameMsg CriarAutorizacaoRequest(MarcacaoExameMsg marcacaoExameRequest)
        {
            return new AutorizacaoExameMsg
            {
                RequestId = marcacaoExameRequest.RequestId,
                CodigoAssociado = marcacaoExameRequest.CodigoAssociado,
                CodigoExame = marcacaoExameRequest.CodigoExame,
                CodigoPlano = marcacaoExameRequest.CodigoPlano,
                DataExame = marcacaoExameRequest.DataExame,
                StatusAssociado = marcacaoExameRequest.StatusAssociado,
                StatusSolicitacao = marcacaoExameRequest.Status
            };
        }

        private static AutorizacaoExameResponse CriarResponseDefault(string message, EStatusSolicitacao status)
        {
            return new AutorizacaoExameResponse
            {
                Status = status,
                Sucess = true,
                Title = message
            };
        }

        private async Task<AutorizacaoExameResponse> GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            return await _bus.RequestAsync<AutorizacaoExameMsg, AutorizacaoExameResponse>(requestMessage);
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
