using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories.Interfaces
{
    public interface IPrestadorRepository
    {
        Task<string> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg);
        Task<bool> GetPlanoConveniado(int codigoPlano);
        Task<List<Plano>> GetAllPlanosConveniados();
    }
}
