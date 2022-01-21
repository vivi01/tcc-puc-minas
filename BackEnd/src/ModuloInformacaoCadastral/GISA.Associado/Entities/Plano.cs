using GISA.Associado.Enums;
using System.ComponentModel.DataAnnotations;

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
        public decimal ValorBase { get; set; }
    }
}
