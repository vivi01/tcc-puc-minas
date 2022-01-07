using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories.Interfaces
{
    public interface IPrestadorRepository : IRepository<Entities.Prestador>
    {
        Task<string> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg);
        Task<bool> GetPlanoConveniado(int codigoPlano);
        Task<List<Plano>> GetAllPlanosConveniados();
    }
}
