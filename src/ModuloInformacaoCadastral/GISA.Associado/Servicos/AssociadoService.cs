using GISA.Associado.Enums;
using GISA.Associado.Repositorios.Interfaces;
using GISA.Associado.Servicos.Interfaces;
using System.Threading.Tasks;

namespace GISA.Associado.Servicos
{
    public class AssociadoService : IAssociadoService
    {
        public IAssociadoRepository _associdoRepository;

        public AssociadoService(IAssociadoRepository associadoRepository)
        {
            _associdoRepository = associadoRepository;
        }

        public async Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado)
        {
            return await _associdoRepository.GetSituacao(codigoAssociado);
        }
    }
}
