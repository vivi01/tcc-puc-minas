using GISA.Prestador.Context;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Repositories
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {
        public PlanoRepository(PrestadorContext context) : base(context)
        {

        }
    }
}
