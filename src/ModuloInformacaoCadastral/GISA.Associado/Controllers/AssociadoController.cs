using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GISA.Associado.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssociadoController : ControllerBase
    {
        private readonly IAssociadoService _associadoService;
        private readonly IPlanoService _planoService;
        // private readonly IDataProtectionProvider _dataProtector;

        public AssociadoController(IAssociadoService associadoService, IPlanoService planoService)
        {
            _associadoService = associadoService;
            _planoService = planoService;
        }

        [HttpGet("[action]/{codigoAssociado}")]
        [ProducesResponseType(typeof(Entities.Associado), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Associado>> GetAssociado(int codigoAssociado)
        {
            var associado = await _associadoService.GetAssociadoByCodigo(codigoAssociado);

            return Ok(associado ?? new Entities.Associado());
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<Plano>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Plano>>> GetTodosPlanosDisponiveis()
        {
            var planos = await _planoService.ObterTodos();

            return Ok(planos ?? new List<Plano>());
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> CadastrarAssociado([FromBody] Entities.Associado associado)
        {
            var result = await _associadoService.CadastrarAssociado(associado);

            if (!result)
                return BadRequest(new { Message = "Erro ao realizar Cadastro" });

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> AlterarPlano(string token, int codigoNovoPlano, bool planoOdontologico)
        {
            var result = await _associadoService.AlterarPlano(token, codigoNovoPlano, planoOdontologico);

            if (!result)
                return BadRequest(new { Message = "Erro ao alterar plano" });

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> SolicitarMarcacaoExame([FromBody] AutorizacaoExameMsg autorizacaoExameMsg)
        {
            return Ok(await _associadoService.SolicitarMarcacaoExame(autorizacaoExameMsg));
        }
    }
}
