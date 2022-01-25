using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.Prestador.Command;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public readonly IPrestadorRepository _prestadorRepository;
        private readonly IPlanoService _planoService;
        private readonly IRabbitManager _manager;
        private readonly IMediator _mediator;

        public PrestadorService(IPrestadorRepository prestadorRepository, IPlanoService planoService,
            IRabbitManager manager, IMediator mediator)
        {
            _prestadorRepository = prestadorRepository;
            _planoService = planoService;
            _manager = manager;
            _mediator = mediator;
        }
        public async Task<string> SolicitarAutorizacoExame(AutorizacaoExameCommand autorizacaoExameMsg)
        {
            if (autorizacaoExameMsg.StatusAssociado != "Ativo")
            {
                return "Nao Autorizado! Associado não está ativo";
            }

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado == null)
            {
                return "Nao Autorizado! Plano não conveniado";
            }

            //chama o legado SGPS para solicitar a autorização do exame
            var result = await GetAutorizacaoExame(autorizacaoExameMsg);

            if (result != "Autorizado")
            {
                return "Nao Autorizado";
            }

            return "Autorizado";
        }

        private async Task<string> GetAutorizacaoExame(AutorizacaoExameCommand requestMessage)
        {
            _manager.Publish<AutorizacaoExameCommand>(requestMessage, "", "", EventBusConstants.GisaQueue);
            return await _mediator.Send(requestMessage);
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
