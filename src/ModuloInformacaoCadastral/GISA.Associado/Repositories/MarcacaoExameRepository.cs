﻿using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class MarcacaoExameRepository : Repository<MarcacaoExame>, IMarcacaoExameRepository
    {
        public MarcacaoExameRepository(AssociadoContext context) : base(context)
        {

        }
    }
}