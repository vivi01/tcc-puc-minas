using GISA.Associado.Entities;
using System.Collections.Generic;

namespace GISA.Associado.Services.Interfaces
{
    public interface IEnderecoService
    {
        bool Adicionar(Endereco endereco);

        bool Editar(Endereco endereco);

        Endereco ObterPorId(int id);

        bool Deletar(Endereco endereco);

        List<Endereco> ObterTodos();
    }
}
