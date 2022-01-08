using GISA.Associado.Context;
using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class AssociadoRepository : Repository<Entities.Associado>, IAssociadoRepository
    {
        public AssociadoRepository(AssociadoContext context) : base(context)
        {

        }
        public async Task<ESituacaoAssociado> GetSituacao(int codigoAssociado)
        {
            var situacao = ESituacaoAssociado.Ativo;
            return JsonConvert.DeserializeObject<ESituacaoAssociado>(situacao.ToString());          
        }

        public async Task<decimal> GetValorPlano()
        {
            return 0;
        }        

        public async Task<bool> AlterarPlano(Entities.Associado associado)
        {
            return true;
        }

        public async Task<List<Plano>> GetPlanosDisponiveis()
        {
            List<Plano> planos = new List<Plano>();
            return planos;
        }

        public Task<Entities.Associado> GetAssociado(int codigoAssociado)
        {
            throw new System.NotImplementedException();
        }

        public void SalvarMarcacaoExame(Entities.Associado associado)
        {
            throw new System.NotImplementedException();
        }
    }
}
