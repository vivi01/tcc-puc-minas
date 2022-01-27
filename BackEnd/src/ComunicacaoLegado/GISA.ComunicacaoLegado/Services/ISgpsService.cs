using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        AutorizacaoExameResponse AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg);
    }
}
