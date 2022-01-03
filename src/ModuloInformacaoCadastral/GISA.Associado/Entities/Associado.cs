using GISA.Associado.Entities;
using GISA.Associado.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Entities
{
    public class Associado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CpfCnpj { get; set; }
        public Endereco Endereco { get; set; }
        public string Formacao { get; set; }
        public Plano Plano { get; set; }
        public int CodigoAssociado { get; set; }
        public List<MarcacaoExame> MarcacaoExames { get; set; }
        public Decimal ValorPlano { get; set; }
    }
}
