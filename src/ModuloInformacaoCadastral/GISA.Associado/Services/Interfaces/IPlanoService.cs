using GISA.Associado.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IPlanoService
    {
        bool Adicionar(Plano plano);

        bool Editar(Plano plano);

        Plano ObterPlanoPorCodigo(int codigo);

        bool Deletar(Plano plano);

        Task<List<Plano>> ObterTodos();
    }
}
