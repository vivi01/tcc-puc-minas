using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.EventBusRabbitMQ.Messages;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
        Task<Entities.Associado> GetAssociadoByUserName(string userName);
        Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado);
        Task GetSituacaoAssociado(AssociadoMsg requestMessage);
        Task<decimal> GetValorPlano();
        Task<bool> AlterarPlano(AlterarPlano alterarPlano);
        Task<string> SolicitarMarcacaoExame(AutorizacaoExame request);
        Task<bool> CadastrarAssociado(Entities.Associado associado);
    }
}
