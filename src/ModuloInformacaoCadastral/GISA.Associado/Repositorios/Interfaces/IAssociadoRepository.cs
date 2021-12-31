using GISA.Associado.Enums;
using System.Threading.Tasks;

namespace GISA.Associado.Repositorios.Interfaces
{
    public interface IAssociadoRepository
    {
        Task<ESituacaoAssociado> GetSituacao(int codigoAssociado);
    }
}
