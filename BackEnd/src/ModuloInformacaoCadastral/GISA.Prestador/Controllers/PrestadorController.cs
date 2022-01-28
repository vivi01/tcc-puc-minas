using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using GISA.Prestador.Entities;
using GISA.Prestador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GISA.Prestador.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrestadorController : ControllerBase
    {
        private readonly IPrestadorService _prestadorService;

        public PrestadorController(IPrestadorService prestadorService)
        {
            _prestadorService = prestadorService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<bool>> CadastrarPrestador([FromBody] Entities.Prestador prestador)
        {
            var result = await _prestadorService.CadastrarPrestador(prestador);

            if (!result)
                return BadRequest(new { Message = "Erro ao realizar Cadastro do Prestador" });

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AutorizacaoExameResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutorizacaoExameResponse), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AutorizacaoExameResponse>> SolicitarAutorizacaoExame([FromBody] MarcacaoExameMsg marcacaoExameRequest)
        {
            var result = await _prestadorService.SolicitarAutorizacaoExame(marcacaoExameRequest);

            if (result == null)
                return BadRequest(new { Message = "Erro ao tentar autorizar exame" });

            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Plano>>> GetAllPlanosConveniados()
        {
            var planos = await _prestadorService.GetAllPlanosConveniados();

            return Ok(planos ?? new List<Plano>());
        }
    }
}
