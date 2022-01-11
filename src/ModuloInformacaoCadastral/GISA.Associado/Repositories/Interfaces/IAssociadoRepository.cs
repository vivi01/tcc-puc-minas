using GISA.Associado.Enums;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IAssociadoRepository : IRepository<Entities.Associado>
    {
        Task<ESituacaoAssociado> GetSituacao(int codigoAssociado);
        Task<Entities.Associado> GetAssociadoByUserName(string userName);
        Task<Entities.Associado> GetAssociado(int codigoAssociado);      
        Task<decimal> GetValorPlano();
        Task SalvarMarcacaoExame(Entities.Associado associado);
    }
}
