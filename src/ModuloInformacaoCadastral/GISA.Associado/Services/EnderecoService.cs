using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class EnderecoService : IEnderecoService
    {
        public IEnderecoRepository _enderecoRepository;
        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;
        }

        public Task<bool> Adicionar(Endereco endereco)
        {
            return _enderecoRepository.Add(endereco);
        }

        public Task<bool> Editar(Endereco endereco)
        {
            return _enderecoRepository.Update(endereco);
        }

        public Task<Endereco> ObterPorId(int id)
        {
            return _enderecoRepository.GetById(id);
        }

        public Task<bool> Deletar(Endereco endereco)
        {
            return _enderecoRepository.Delete(endereco);
        }

        public Task<List<Endereco>> ObterTodos()
        {
            return _enderecoRepository.Get();
        }
    }
}
