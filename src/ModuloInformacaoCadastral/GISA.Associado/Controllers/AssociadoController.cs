using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Events;
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
        private IAssociadoService _associadoService;

        public AssociadoController(IAssociadoService associadoService)
        {
            _associadoService = associadoService;
        }

        [HttpGet("[action]/{codigoAssociado}")]
        [ProducesResponseType(typeof(Entities.Associado), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Associado>> GetAssociado(int codigoAssociado)
        {
            var associado = await _associadoService.GetAssociado(codigoAssociado);

            return Ok(associado ?? new Entities.Associado());
        }

        [HttpGet("[action]")]
        [ProducesResponseType(typeof(List<Plano>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Associado>> GetTodosPlanosDisponiveis()
        {
            var planos = await _associadoService.GetTodosPlanosDisponiveis();

            return Ok(planos ?? new List<Plano>());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Entities.Associado>> AlterarPlano([FromBody] Entities.Associado associado)
        {
            return Ok(await _associadoService.AlterarPlano(associado));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SolicitarMarcacaoExame([FromBody] AutorizacaoExameMsg autorizacaoExameMsg)
        {
            return Ok(await _associadoService.SolicitarMarcacaoExame(autorizacaoExameMsg));
        }
    }
}
