using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public bool Adicionar(Plano plano)
        {
            return _planoRepository.Add(plano);
        }

        public bool Editar(Plano plano)
        {
            return _planoRepository.Update(plano);
        }

        public Plano ObterPlanoPorCodigo(int codigo)
        {
            return _planoRepository.GetById(codigo);
        }

        public bool Deletar(Plano plano)
        {
            return _planoRepository.Delete(plano);
        }

        public async Task<List<Plano>> ObterTodos()
        {
            return _planoRepository.Get().ToList();
        }
    }
}
