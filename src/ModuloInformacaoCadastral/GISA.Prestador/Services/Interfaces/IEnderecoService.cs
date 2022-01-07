using GISA.Prestador.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Services.Interfaces
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
