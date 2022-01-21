using GISA.Associado.Entities;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IPlanoRepository : IRepository<Plano>
    {
        Task<Plano> ObterPlanoPorCodigo(int codigoPlano);
    }
}
