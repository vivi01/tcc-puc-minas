using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;

namespace GISA.Associado.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(AssociadoContext context) : base(context)
        {

        }
    }
}
