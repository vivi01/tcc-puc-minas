using GISA.Associado.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Entities
{
    public class Plano
    {
        public int Id { get; set; }
        public int CodigoPlano { get; set; }
        public string Descricao { get; set; }
        public ETipoPlano TipoPlano { get; set; }
        public EClassificacaoPlano ClassificacaoPlano { get; set; }
    }
}
