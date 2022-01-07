using GISA.EventBusRabbitMQ.Interfaces;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Prestador.Services
{
    public class EnderecoService : IEnderecoService
    {
        public IEnderecoRepository _enderecoRepository;
        private IBus _busControl;

        public EnderecoService(IEnderecoRepository enderecoRepository, IBus busControl)
        {
            _enderecoRepository = enderecoRepository;
            _busControl = busControl;
        }

        public void Adicionar(Endereco endereco)
        {
            _enderecoRepository.Add(endereco);
        }

        public void Editar(Endereco endereco)
        {
            _enderecoRepository.Update(endereco);
        }

        public Endereco ObterPorId(int id)
        {
            return _enderecoRepository.GetById(id);
        }

        public void Deletar(Endereco endereco)
        {
            _enderecoRepository.Delete(endereco);
        }

        public List<Endereco> ObterTodos()
        {
            return _enderecoRepository.Get().ToList();
        }
    }
}
