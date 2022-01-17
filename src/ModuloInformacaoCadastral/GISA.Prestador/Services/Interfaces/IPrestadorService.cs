using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
{
    public interface IPrestadorService
    {
        Task<string> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg);
        Task<List<Plano>> GetAllPlanosConveniados();
        Task<bool> CadastrarPrestador(Entities.Prestador prestador);
        Task<Plano> GetPlanoConveniado(int codigoPlano);
    }
}
