using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class AssociadoRepository : IAssociadoRepository
    {
        public async Task<ESituacaoAssociado> GetSituacao(int codigoAssociado)
        {
            var situacao = ESituacaoAssociado.Ativo;
            return JsonConvert.DeserializeObject<ESituacaoAssociado>(situacao.ToString());
           
        }

        public async Task<decimal> GetValor()
        {
            return 0;
        }

        //public async Task<string> SolicitarMarcacaoExame(MarcacaoConsulta marcacaoConsulta, string token)
        //{

        //}

        public async void AlterarPlano(Entities.Associado associado, string token)
        {

        }

        public async Task<List<Plano>> Getplanosdisponoveis()
        {
            List<Plano> planos = new List<Plano>();
            return planos;
        }
    }
}
