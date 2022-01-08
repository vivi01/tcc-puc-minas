using System;
using System.Collections.Generic;

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
        public decimal ValorPlano { get; set; }
        public string Email { get; set; }
        public bool PossuiPlanoOdontologico { get; set; }
    }
}
