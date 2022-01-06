using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class PrestadorRepository : IPrestadorRepository
    {
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
