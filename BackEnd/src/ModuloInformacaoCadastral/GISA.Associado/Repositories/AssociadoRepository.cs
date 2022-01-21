using GISA.Associado.Context;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories
{
    public class AssociadoRepository : Repository<Entities.Associado>, IAssociadoRepository
    {
        public AssociadoRepository(AssociadoContext context) : base(context)
        {

        }

        public Task<Entities.Associado> GetAssociado(int codigoAssociado)
        {
            return
                _context.Associados.Where(b => b.CodigoAssociado.Equals(codigoAssociado)).FirstOrDefaultAsync();

        }

        public async Task<ESituacaoAssociado> GetSituacao(int codigoAssociado)
        {
            var associado = await
                _context.Associados.Where(b => b.CodigoAssociado.Equals(codigoAssociado)).FirstOrDefaultAsync();
            return associado.SituacaoAssociado;
        }

        public Task<Entities.Associado> GetAssociadoByUserName(string userName)
        {
            return
               _context.Associados.Where(b => b.UserName.Equals(userName)).FirstOrDefaultAsync();
        }

        public async Task<decimal> GetValorPlano()
        {
            return 0;
        }

        public Task SalvarMarcacaoExame(Entities.Associado associado)
        {
            throw new System.NotImplementedException();
        }
    }
}
