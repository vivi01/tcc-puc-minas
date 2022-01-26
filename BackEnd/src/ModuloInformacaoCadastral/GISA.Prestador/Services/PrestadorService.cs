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

        public PrestadorService(IPrestadorRepository prestadorRepository, IPlanoService planoService,
           IMessageBus bus)
        {
            _prestadorRepository = prestadorRepository;
            _planoService = planoService;
            _bus = bus;
        }
        public async Task<DefaultResponse> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            if (autorizacaoExameMsg.StatusAssociado != "Ativo")
            {
                return CriarResponseDefault("Nao Autorizado! Associado não está ativo", "Não Autorizado");
            }

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado == null)
            {
                return CriarResponseDefault("Nao Autorizado! Plano não conveniado", "Não Autorizado");
            }

            //chama o legado SGPS para solicitar a autorização do exame
            return await GetAutorizacaoExame(autorizacaoExameMsg);
        }

        private DefaultResponse CriarResponseDefault(string message, string status)
        {
            return new DefaultResponse
            {
                Status = status,
                Sucess = true,
                Title = message
            };
        }

        private async Task<DefaultResponse> GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            return await _bus.RequestAsync<AutorizacaoExameMsg, DefaultResponse>(requestMessage);
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
