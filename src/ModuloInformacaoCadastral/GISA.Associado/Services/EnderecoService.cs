using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace GISA.Associado.Services
{
    public class EnderecoService : IEnderecoService
    {
        public IEnderecoRepository _enderecoRepository;       
        public EnderecoService(IEnderecoRepository enderecoRepository)
        {
            _enderecoRepository = enderecoRepository;            
        }

        public bool Adicionar(Endereco endereco)
        {
           return _enderecoRepository.Add(endereco);
        }

        public bool Editar(Endereco endereco)
        {
            return _enderecoRepository.Update(endereco);
        }

        public Endereco ObterPorId(int id)
        {
            return _enderecoRepository.GetById(id);
        }

        public bool Deletar(Endereco endereco)
        {
            return _enderecoRepository.Delete(endereco);
        }

        public List<Endereco> ObterTodos()
        {
            return _enderecoRepository.Get().ToList();
        }
    }
}
