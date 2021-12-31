using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.ModeloMensagens;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        void AutorizarExame(AutorizacaoExameMsg requestMessage);       
    }
}
