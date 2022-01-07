using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class PrestadorRepository : Repository<Entities.Prestador>, IPrestadorRepository
    {
        public PrestadorRepository(PrestadorContext context) : base(context)
        {

        }
        public async Task<string> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg)
        {
            return "autorização";
        }

        public async Task<bool> GetPlanoConveniado(int codigoPlano)
        {
            return true;
        }

        public async Task<List<Plano>> GetAllPlanosConveniados()
        {
            List<Plano> Planos = new List<Plano>();
            return Planos;
        }
    }
}
