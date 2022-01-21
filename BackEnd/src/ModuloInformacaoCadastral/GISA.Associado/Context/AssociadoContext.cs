using GISA.Associado.Entities;
using Microsoft.EntityFrameworkCore;

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
