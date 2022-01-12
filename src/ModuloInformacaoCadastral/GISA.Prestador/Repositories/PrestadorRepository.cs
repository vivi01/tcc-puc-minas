using GISA.Prestador.Context;
using GISA.Prestador.Repositories.Interfaces;

namespace GISA.Prestador.Repositories
{
    public class PrestadorRepository : Repository<Entities.Prestador>, IPrestadorRepository
    {
        public PrestadorRepository(PrestadorContext context) : base(context)
        {

        }       
    }
}
