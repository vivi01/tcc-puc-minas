using GISA.Associado.Enums;
using System.Threading.Tasks;

namespace GISA.Associado.Servicos.Interfaces
{
    public interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
    }
}
