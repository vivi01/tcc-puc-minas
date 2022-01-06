using GISA.Associado.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Context
{
    public class AssociadoContext : DbContext
    {
        public AssociadoContext(DbContextOptions<AssociadoContext> options) : base(options)
        {

        }
        public DbSet<Entities.Associado> Associados { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<MarcacaoExame> MarcacaoExames { get; set; }
        public DbSet<Plano> Planos { get; set; }
    }
}
