using GISA.Associado.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IMarcacaoExameService
    {
        Task<bool> Adicionar(MarcacaoExame marcacao);

        Task<bool> Editar(MarcacaoExame marcacao);

        Task<MarcacaoExame> ObterPorId(int id);

        Task<bool> Deletar(MarcacaoExame marcacao);

        Task<List<MarcacaoExame>> ObterTodos();
    }
}
