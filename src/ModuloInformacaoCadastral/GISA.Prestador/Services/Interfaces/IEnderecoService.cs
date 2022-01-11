using GISA.Prestador.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
{
    public interface IEnderecoService
    {
        Task<bool> Adicionar(Endereco endereco);

        Task<bool> Editar(Endereco endereco);

        Task<Endereco> ObterPorId(int id);

        Task<bool> Deletar(Endereco endereco);

        Task<List<Endereco>> ObterTodos();

    }
}
