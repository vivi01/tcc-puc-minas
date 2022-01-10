using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository;
        private readonly IPlanoService _planoService;
        private readonly IBus _busControl;

        public AssociadoService(IAssociadoRepository associadoRepository, IBus busControl, IPlanoService planoService)
        {
            _associadoRepository = associadoRepository;
            _busControl = busControl;
            _planoService = planoService;
        }

        public async Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado)
        {
            return await _associadoRepository.GetSituacao(codigoAssociado);
        }

        public async Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado)
        {
            return await _associadoRepository.GetAssociado(codigoAssociado);
        }

        public async Task<Entities.Associado> GetAssociadoByUserName(string userName)
        {
            return await _associadoRepository.GetAssociadoByUserName(userName);
        }

        public async Task<decimal> GetValorPlano()
        {
            return await _associadoRepository.GetValorPlano();
        }

        public async Task<List<Plano>> GetTodosPlanosDisponiveis()
        {
            return await _associadoRepository.GetPlanosDisponiveis();
        }

        public async Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg request)
        {
            var associado = await GetAssociadoByCodigo(request.CodigoAssociado);

            if (associado == null)
                return "Associado Não Encontrado";

            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.PrestadorExchange, request);

            if (request.Status != "Autorizado")
                return request.MensagensErro;

            associado.MarcacaoExames.Add(new MarcacaoExame
            {
                DataExame = request.DataExame,
                CodigoExame = request.CodigoExame
            });

            _associadoRepository.SalvarMarcacaoExame(associado);

            return "Marcação realizada com Sucesso!";
        }

        public async Task<bool> AlterarPlano(string token, int codigoNovoPlano, bool planoOdontologico)
        {
            var request = new AuthTokenMsg(token);
           
            await _busControl.SendAsync<AuthTokenMsg>(EventBusConstants.AutenticacaoExchange, request);

            if (string.IsNullOrWhiteSpace(request.UserName))
                return false;

            var usuario = await GetAssociadoByUserName(request.UserName);

            var plano = _planoService.ObterPlanoPorCodigo(codigoNovoPlano);

            usuario.Plano = plano;
            usuario.PossuiPlanoOdontologico = planoOdontologico;

            return _associadoRepository.Update(usuario);
        }
    }
}
