using GISA.Associado.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GISA.Associado.Entities
{
    public class Plano
    {
        [Key]
        public int Id { get; set; }
        public int CodigoPlano { get; set; }
        public string Descricao { get; set; }
        public ETipoPlano TipoPlano { get; set; }
        public EClassificacaoPlano ClassificacaoPlano { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal ValorBase { get; set; }
    }
}
