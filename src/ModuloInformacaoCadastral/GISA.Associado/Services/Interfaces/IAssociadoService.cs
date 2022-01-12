using GISA.Associado.Enums;
using GISA.EventBusRabbitMQ.Events;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
        Task<Entities.Associado> GetAssociadoByUserName(string userName);
        Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado);
        Task<decimal> GetValorPlano();
        Task<bool> AlterarPlano(string token, int codigoNovoPlano, bool planoOdontologico);
        Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg request);
        Task<bool> CadastrarAssociado(Entities.Associado associado);
    }
}
