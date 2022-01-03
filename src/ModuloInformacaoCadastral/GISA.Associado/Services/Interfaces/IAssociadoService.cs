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
        Task<Entities.Associado> GetAssociado(int codigoAssociado);
        Task<decimal> GetValorPlano();
        Task<bool> AlterarPlano(Entities.Associado associado);
        Task<List<Plano>> GetTodosPlanosDisponiveis();
        Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg request);
    }
}
