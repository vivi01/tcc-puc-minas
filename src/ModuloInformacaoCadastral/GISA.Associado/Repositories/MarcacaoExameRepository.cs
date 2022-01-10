using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;

namespace GISA.Associado.Repositories
{
    public class MarcacaoExameRepository : Repository<MarcacaoExame>, IMarcacaoExameRepository
    {
        public MarcacaoExameRepository(AssociadoContext context) : base(context)
        {

        }
    }
}
