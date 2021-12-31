using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        public IAssociadoRepository _associadoRepository;

        public AssociadoService(IAssociadoRepository associadoRepository)
        {
            _associadoRepository = associadoRepository;
        }

        public async Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado)
        {
            return await _associadoRepository.GetSituacao(codigoAssociado);
        }


        public async Task<decimal> GetValorPlano()
        {
            return await _associadoRepository.GetValor();
        }

        //public void SolicitarMarcacaoExame(MarcacaoConsulta marcacaoConsulta, string token)
        //{

        //}

        public async void AlterarPlano(Entities.Associado associado, string token)
        {
             _associadoRepository.AlterarPlano(associado, token);
        }

        public async Task<List<Plano>> GetTodosPlanosDisponoveis()
        {
            return await _associadoRepository.Getplanosdisponoveis();
        }
    }
}
