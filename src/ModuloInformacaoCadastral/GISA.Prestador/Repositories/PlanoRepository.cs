using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;

namespace GISA.Prestador.Repositories
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {
        public PlanoRepository(PrestadorContext context) : base(context)
        {

        }
    }
}
