using GISA.Associado.Enums;
using GISA.Associado.Repositorios.Interfaces;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace GISA.Associado.Repositorios
{
    public class AssociadoRepository : IAssociadoRepository
    {
        public async Task<ESituacaoAssociado> GetSituacao(int codigoAssociado)
        {
            var situacao = ESituacaoAssociado.Ativo;
            return JsonConvert.DeserializeObject<ESituacaoAssociado>(situacao.ToString());
           
        }
    }
}
