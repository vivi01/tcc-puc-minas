using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class MarcacaoExameService : IMarcacaoExameService
    {
        public IMarcacaoExameRepository _marcacaoExameRepository;       

        public MarcacaoExameService(IMarcacaoExameRepository marcacaoExameRepository)
        {
            _marcacaoExameRepository = marcacaoExameRepository;            
        }

        public Task<bool> Adicionar(MarcacaoExame marcacaoExame)
        {
            return _marcacaoExameRepository.Add(marcacaoExame);
        }

        public Task<bool> Editar(MarcacaoExame marcacaoExame)
        {
            return _marcacaoExameRepository.Update(marcacaoExame);
        }

        public Task<MarcacaoExame> ObterPorId(int id)
        {
            return _marcacaoExameRepository.GetById(id);
        }

        public Task<bool> Deletar(MarcacaoExame marcacaoExame)
        {
            return _marcacaoExameRepository.Delete(marcacaoExame);
        }

        public Task<List<MarcacaoExame>> ObterTodos()
        {
            return _marcacaoExameRepository.Get();
        }
    }
}
