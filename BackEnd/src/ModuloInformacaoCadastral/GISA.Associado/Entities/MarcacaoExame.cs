using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GISA.Associado.Entities
{
    public class MarcacaoExame
    {
        [Key]
        public int Id { get; set; }
        public DateTime DataExame { get; set; }
        public int CodigoExame { get; set; }
        public List<Associado> Associados { get; set; } = new List<Associado>();
    }
}
