using GISA.Associado.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GISA.Associado.Entities
{
    public class Associado
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CpfCnpj { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
        public string Formacao { get; set; }
        public virtual Plano Plano { get; set; }
        public int PlanoId { get; set; }
        public int CodigoAssociado { get; set; }
        public List<MarcacaoExame> MarcacaoExames { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorPlano { get; set; }
        public string Email { get; set; }
        public bool PossuiPlanoOdontologico { get; set; }
        public ESituacaoAssociado SituacaoAssociado { get; set; }
        public string UserName { get; set; }
    }
}
