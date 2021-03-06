using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {
        public PlanoRepository(PrestadorContext context) : base(context)
        {

        }
        public Task<Plano> ObterPlanoPorCodigo(int codigoPlano)
        {
            return _context.Planos.Where(b => b.CodigoPlano.Equals(codigoPlano)).FirstOrDefaultAsync();
        }
    }
}
