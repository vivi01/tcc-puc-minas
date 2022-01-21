using GISA.Prestador.Entities;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories.Interfaces
{
    public interface IPlanoRepository : IRepository<Plano>
    {
        Task<Plano> ObterPlanoPorCodigo(int codigoPlano);
    }
}
