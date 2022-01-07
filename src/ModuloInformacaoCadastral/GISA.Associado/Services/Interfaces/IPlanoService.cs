using GISA.Associado.Entities;
using System.Collections.Generic;

namespace GISA.Associado.Services.Interfaces
{
    public interface IPlanoService
    {
        void Adicionar(Plano plano);

        void Editar(Plano plano);

        Plano ObterPorId(int id);

        void Deletar(Plano plano);

        List<Plano> ObterTodos();
    }
}
