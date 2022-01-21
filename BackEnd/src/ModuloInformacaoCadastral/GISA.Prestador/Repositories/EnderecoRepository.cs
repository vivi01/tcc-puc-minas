using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;

namespace GISA.Prestador.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(PrestadorContext context) : base(context)
        {

        }
    }
}
