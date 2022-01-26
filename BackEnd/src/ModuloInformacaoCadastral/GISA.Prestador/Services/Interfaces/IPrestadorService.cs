using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using GISA.Prestador.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
{
    public interface IPrestadorService
    {
        Task<DefaultResponse> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg);
        Task<List<Plano>> GetAllPlanosConveniados();
        Task<bool> CadastrarPrestador(Entities.Prestador prestador);
        Task<Plano> GetPlanoConveniado(int codigoPlano);
    }
}
