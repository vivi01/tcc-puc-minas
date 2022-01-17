using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.Services
{
    public class SgpsService : ISgpsService
    {
        private readonly IBus _busControl;

        public SgpsService(IBus busControl)
        {
            _busControl = busControl;
        }

        public async void AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            //Change to receive
            await _busControl.ReceiveAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue,
               x =>
               {
                   //Criado um mock sempre vai retornar True.. numa situação real seria necessário verificar
                   //se o solicitante é conveniado e se o associado está adimplente
                   Task.Run(() => { GetAutorizacao(autorizacaoExameMsg); });
               });

        }

        private static void GetAutorizacao(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            autorizacaoExameMsg.Status = "Autorizado";          
        }
    }
}
