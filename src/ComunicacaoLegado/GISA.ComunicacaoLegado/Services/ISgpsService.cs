using GISA.EventBusRabbitMQ.Events;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.Services
{
    public interface ISgpsService
    {
        void AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg);
    }
}
