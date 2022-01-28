using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        Task<AutorizacaoExameResponse> AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg);
    }
}
