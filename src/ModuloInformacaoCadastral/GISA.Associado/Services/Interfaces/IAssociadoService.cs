using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.EventBusRabbitMQ.Events;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
        Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado);
        Task<decimal> GetValorPlano();
        Task<bool> AlterarPlano(string token, int codigoNovoPlano, bool planoOdontologico);
        Task<List<Plano>> GetTodosPlanosDisponiveis();
        Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg request);
    }
}
