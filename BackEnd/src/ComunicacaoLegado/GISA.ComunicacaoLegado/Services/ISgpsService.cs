using GISA.EventBusRabbitMQ.Messages;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        void AutorizarExame(AutorizacaoExame autorizacaoExameMsg);
    }
}
