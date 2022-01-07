using GISA.Associado.Entities;
using System.Collections.Generic;

namespace GISA.Associado.Services.Interfaces
{
    public interface IEnderecoService
    {
        void Adicionar(Endereco endereco);

        void Editar(Endereco endereco);

        Endereco ObterPorId(int id);

        void Deletar(Endereco endereco);

        List<Endereco> ObterTodos();
    }
}
