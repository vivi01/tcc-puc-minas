using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class PlanoService : IPlanoService
    {
        public IPlanoRepository _planoRepository;

        public PlanoService(IPlanoRepository planoRepository)
        {
            _planoRepository = planoRepository;
        }

        public Task<bool> Adicionar(Plano plano)
        {
            return _planoRepository.Add(plano);
        }

        public Task<bool> Editar(Plano plano)
        {
            return _planoRepository.Update(plano);
        }

        public Task<Plano> ObterPlanoPorCodigo(int codigo)
        {
            return _planoRepository.GetById(codigo);
        }

        public Task<bool> Deletar(Plano plano)
        {
            return _planoRepository.Delete(plano);
        }

        public Task<List<Plano>> ObterTodos()
        {
            return _planoRepository.Get();
        }
    }
}
