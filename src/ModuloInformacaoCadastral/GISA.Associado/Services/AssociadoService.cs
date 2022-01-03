using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        public IAssociadoRepository _associadoRepository;
        private IBus _busControl;

        public AssociadoService(IAssociadoRepository associadoRepository, IBus busControl)
        {
            _associadoRepository = associadoRepository;
            _busControl = busControl;
        }

        public async Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado)
        {
            return await _associadoRepository.GetSituacao(codigoAssociado);
        }

        public async Task<Entities.Associado> GetAssociado(int codigoAssociado)
        {
            return await _associadoRepository.GetAssociado(codigoAssociado);
        }

        public async Task<decimal> GetValorPlano()
        {
            return await _associadoRepository.GetValor();
        }

        public async Task<List<Plano>> GetTodosPlanosDisponiveis()
        {
            return await _associadoRepository.GetPlanosDisponiveis();
        }

        public async Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg request)
        {
            var associado = await GetAssociado(request.CodigoAssociado);

            if (associado == null)
                return "Associado Não Encontrado";

            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.PrestadorExchange, request);

            if (request.Status != "Autorizado")
                return request.MensagensErro;

            associado.MarcacaoExames.Add(new MarcacaoExame
            {
                DataExame = DateTime.Now,
                CodigoExame = request.CodigoExame
            });

            _associadoRepository.SalvarMarcacaoExame(associado);

            return "Marcação realizada com Sucesso!";
        }

        public async Task<bool> AlterarPlano(Entities.Associado associado)
        {
            return await _associadoRepository.AlterarPlano(associado);
        }             
    }
}
