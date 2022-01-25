using GISA.Prestador.Command;
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> SolicitarAutorizacoExame([FromBody] AutorizacaoExameCommand autorizacaoExameMsg)
        {
            return Ok(await _prestadorService.SolicitarAutorizacoExame(autorizacaoExameMsg));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Plano>>> GetAllPlanosConveniados()
        {
            var planos = await _prestadorService.GetAllPlanosConveniados();

            return Ok(planos ?? new List<Plano>());
        }
    }
}
