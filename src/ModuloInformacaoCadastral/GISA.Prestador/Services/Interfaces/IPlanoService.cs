using GISA.Prestador.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
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
