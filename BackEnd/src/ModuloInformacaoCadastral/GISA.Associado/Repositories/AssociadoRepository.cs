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
            try
            {
                return
               _context.Associados.Where(b => b.CodigoAssociado.Equals(codigoAssociado)).Include(x => x.Plano).FirstOrDefaultAsync();
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
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

        public async Task<decimal> GetValorPlano(int codigoAssociado)
        {
            var associado = await GetAssociado(codigoAssociado);

            return associado.ValorPlano;
        }
    }
}
