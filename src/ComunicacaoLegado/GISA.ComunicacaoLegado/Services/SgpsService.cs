using GISA.ComunicacaoLegado.Enums;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;

namespace GISA.ComunicacaoLegado.Services
{
    public class SgpsService : ISgpsService
    {
        private IBus _busControl;

        public SgpsService(IBus busControl)
        {
            _busControl = busControl;
        }     

        public void AutorizarExame(AutorizacaoExameMsg requestMessage)
        {
            if (requestMessage.Token == null)
            {
                requestMessage.MensagensErro = "Usuário Não Autorizado";
            }
            else
            {
                //Criado um mock sempre vai retornar True.. numa situação real seria necessário verificar
                //se o solicitante é conveniado e se o associado está adimplente
                requestMessage.Status = EStatusExame.Autorizado.ToString();
                _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.PrestadorExchange, requestMessage);
            }
        }
    }
}
