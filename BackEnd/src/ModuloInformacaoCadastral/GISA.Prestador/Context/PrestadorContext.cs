using GISA.Prestador.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Context
{
    public class PrestadorContext : DbContext
    {
        public PrestadorContext(DbContextOptions<PrestadorContext> options) : base(options)
        {

        }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Entities.Prestador> Prestadores { get; set; }
    }
}
