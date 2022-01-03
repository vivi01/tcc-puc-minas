using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
{
    public interface IPrestadorService
    {
        Task<string> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg);
        Task<bool> GetPlanosConveniados(int codigoPlano);
        Task<List<Plano>> GetAllPlanosConveniados();
    }
}
