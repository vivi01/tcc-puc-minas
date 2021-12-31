using GISA.Associado.Entities;
using GISA.Associado.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Repositories.Interfaces
{
    public interface IAssociadoRepository
    {
        Task<ESituacaoAssociado> GetSituacao(int codigoAssociado);
        Task<decimal> GetValor();
        //Task<string> SolicitarMarcacaoExame(MarcacaoConsulta marcacaoConsulta, string token);
        void AlterarPlano(Entities.Associado associado, string token);
        Task<List<Plano>> Getplanosdisponoveis();
    }
}
