using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(PrestadorContext context) : base(context)
        {

        }
    }
}
