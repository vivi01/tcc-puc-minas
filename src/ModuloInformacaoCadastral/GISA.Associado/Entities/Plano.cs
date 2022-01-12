using GISA.Associado.Enums;

namespace GISA.Associado.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public int CodigoPlano { get; set; }
        public string Descricao { get; set; }
        public ETipoPlano TipoPlano { get; set; }
        public EClassificacaoPlano ClassificacaoPlano { get; set; }        
        public decimal ValorBase { get; set; }
    }
}
