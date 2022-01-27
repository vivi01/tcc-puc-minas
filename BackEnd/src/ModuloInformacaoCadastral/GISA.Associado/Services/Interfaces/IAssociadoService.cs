using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
        Task<Entities.Associado> GetAssociadoByUserName(string userName);
        Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado);        
        Task<decimal> GetValorPlano(int codigoAssociado);
        Task<bool> AlterarPlano(AlterarPlano alterarPlano);
        Task<MarcacaoExameResponse> SolicitarMarcacaoExame(MarcacaoExameMsg request);
        Task<bool> CadastrarAssociado(Entities.Associado associado);
    }
}
