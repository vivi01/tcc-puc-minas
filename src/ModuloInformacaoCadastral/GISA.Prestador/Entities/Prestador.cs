using GISA.Prestador.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Entities
{
    public class Prestador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CpfCnpj { get; set; }
        public Endereco Endereco { get; set; }
        public string Formacao { get; set; }
        public int CodigoPrestador { get; set; }
        public List<Plano> Planos { get; set; }
        public ECategoria Categoria { get; set; }
    }
}
