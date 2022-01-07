using GISA.Associado.Entities;
using GISA.Associado.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IAssociadoRepository : IRepository<Entities.Associado>
    {
        Task<ESituacaoAssociado> GetSituacao(int codigoAssociado);
        Task<Entities.Associado> GetAssociado(int codigoAssociado);
        Task<decimal> GetValorPlano();
        Task<bool> AlterarPlano(Entities.Associado associado);
        Task<List<Plano>> GetPlanosDisponiveis();
        void SalvarMarcacaoExame(Entities.Associado associado);
    }
}
