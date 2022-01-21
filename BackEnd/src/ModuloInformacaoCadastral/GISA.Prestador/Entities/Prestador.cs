using GISA.Prestador.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GISA.Prestador.Entities
{
    [Table("Prestador")]
    public class Prestador
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CpfCnpj { get; set; }
        public virtual Endereco Endereco { get; set; }
        public int EnderecoId { get; set; }
        public string Formacao { get; set; }
        public int CodigoPrestador { get; set; }
        public List<Plano> Planos { get; set; }       
        public ECategoria Categoria { get; set; }
        public string Email { get; set; }
    }
}
