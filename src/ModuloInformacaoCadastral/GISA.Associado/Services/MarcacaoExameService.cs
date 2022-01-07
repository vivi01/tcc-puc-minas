using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class MarcacaoExameService : IMarcacaoExameService
    {
        public IMarcacaoExameRepository _marcacaoExameRepository;
        private IBus _busControl;

        public MarcacaoExameService(IMarcacaoExameRepository marcacaoExameRepository, IBus busControl)
        {
            _marcacaoExameRepository = marcacaoExameRepository;
            _busControl = busControl;
        }

        public void Adicionar(MarcacaoExame marcacaoExame)
        {
            _marcacaoExameRepository.Add(marcacaoExame);
        }

        public void Editar(MarcacaoExame marcacaoExame)
        {
            _marcacaoExameRepository.Update(marcacaoExame);
        }

        public MarcacaoExame ObterPorId(int id)
        {
            return _marcacaoExameRepository.GetById(id);
        }

        public void Deletar(MarcacaoExame marcacaoExame)
        {
            _marcacaoExameRepository.Delete(marcacaoExame);
        }

        public List<MarcacaoExame> ObterTodos()
        {
            return _marcacaoExameRepository.Get().ToList();
        }
    }
}
