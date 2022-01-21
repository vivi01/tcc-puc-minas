using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {
        public PlanoRepository(AssociadoContext context) : base(context)
        {
        }
        public Task<Plano> ObterPlanoPorCodigo(int codigoPlano)
        {
            return _context.Planos.Where(b => b.CodigoPlano.Equals(codigoPlano)).FirstOrDefaultAsync();
        }
    }
}
