using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public IPrestadorRepository _prestadorRepository;
        private IBus _busControl;

        public PrestadorService(IPrestadorRepository prestadorRepository, IBus busControl)
        {
            _prestadorRepository = prestadorRepository;
            _busControl = busControl;
        }
        public async Task<string> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg)
        {
            var message = new AssociadoMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = autorizacaoExameMsg.CodigoAssociado,
                Token = token
            };

            //chama o microsserviço do associado para verificar o status
            GetSituacaoAssociado(message);

            if (message.Status != "Ativo")
                return "Nao Autorizado";

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado != true)
            {
                return "Plano não conveniado";
            }

            //chama o legado SGPS para solicitar a autorização do exame

            GetAutorizacaoExame(autorizacaoExameMsg);

            if (autorizacaoExameMsg.Status != "Autorizado")
                return "Nao Autorizado";

            return "Autorizado";
        }

        private async void GetSituacaoAssociado(AssociadoMsg requestMessage)
        {
            await _busControl.SendAsync<AssociadoMsg>(EventBusConstants.AssociadoExchange, requestMessage);
        }
        private async void GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.PrestadorExchange, requestMessage); ;
        }

        public async Task<bool> GetPlanoConveniado(int codigoPlano)
        {
            return await _prestadorRepository.GetPlanoConveniado(codigoPlano);
        }

        public async Task<List<Plano>> GetAllPlanosConveniados()
        {
            return await _prestadorRepository.GetAllPlanosConveniados();
        }
    }
}
