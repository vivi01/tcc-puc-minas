using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class PlanoRepository : Repository<Plano>, IPlanoRepository
    {
        public PlanoRepository(AssociadoContext context) : base(context)
        {

        }
    }
}
