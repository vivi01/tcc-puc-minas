using GISA.Conveniado.Servicos.Interfaces;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;

namespace GISA.Conveniado.Servicos
{
    public class ConveniadosService : IConveniadosService
    {
        private IBus _busControl;

        public ConveniadosService(IBus busControl)
        {
            _busControl = busControl;
        }
        public string SolicitarAutorizacoExame(string token, int codigoExame, int codigoAssociado)
        {
            var message = new SituacaoAssociadoMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = codigoAssociado,
                Token = token
            };

            //chama o microsserviço do associado para verificar o status
            GetSituacaoAssociado(message);

            if (message.Status != "Ativo")
                return "Nao Autorizado";

            //chama o legado SGPS para solicitar a autorização do exame
            var autorizacaoMsg = new AutorizacaoExameMsg
            {
                RequestId = new System.Guid(),
                CodigoExame = codigoExame,
                Token = token
            };

            GetAutorizacaoExame(autorizacaoMsg);

            if (autorizacaoMsg.Status != "Autorizado")
                return "Nao Autorizado";

            return "Autorizado";
        }

        private async void GetSituacaoAssociado(SituacaoAssociadoMsg requestMessage)
        {
            await _busControl.SendAsync<SituacaoAssociadoMsg>(EventBusConstants.AssociadoExchange, requestMessage);
        }
        private async void GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.ConveniadoExchange, requestMessage); ;
        }
    }
}
