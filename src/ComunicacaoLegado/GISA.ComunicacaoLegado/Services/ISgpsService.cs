using GISA.EventBusRabbitMQ.Events;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        public void AutorizarExame(AutorizacaoExameMsg requestMessage);
    }
}
