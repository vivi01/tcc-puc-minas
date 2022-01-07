using GISA.Associado.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    public interface IMarcacaoExameService
    {
        void Adicionar(MarcacaoExame marcacao);

        void Editar(MarcacaoExame marcacao);

        MarcacaoExame ObterPorId(int id);

        void Deletar(MarcacaoExame marcacao);

        List<MarcacaoExame> ObterTodos();
    }
}
