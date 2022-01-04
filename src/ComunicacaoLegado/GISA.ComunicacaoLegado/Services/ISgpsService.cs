using GISA.EventBusRabbitMQ.Events;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        void AutorizarExame(AutorizacaoExameMsg requestMessage);
    }
}
