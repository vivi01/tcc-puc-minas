using GISA.Associado.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IPlanoService
    {
        Task<bool> Adicionar(Plano plano);

        Task<bool> Editar(Plano plano);

        Task<Plano> ObterPlanoPorCodigo(int codigo);

        Task<bool> Deletar(Plano plano);

        Task<List<Plano>> ObterTodos();
    }
}
