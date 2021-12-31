using GISA.Associado.Entities;
using GISA.Associado.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.Services.Interfaces
{
    interface IAssociadoService
    {
        Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado);
        Task<decimal> GetValorPlano();
        //void SolicitarMarcacaoExame(MarcacaoConsulta marcacaoConsulta, string token)
        void AlterarPlano(Entities.Associado associado, string token);
        Task<List<Plano>> GetTodosPlanosDisponoveis();

    }
}
