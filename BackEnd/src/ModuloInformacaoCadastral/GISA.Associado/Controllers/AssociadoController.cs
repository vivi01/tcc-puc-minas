using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GISA.Associado.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AssociadoController : ControllerBase
    {
        private readonly IAssociadoService _associadoService;
        
        public AssociadoController(IAssociadoService associadoService)
        {
            _associadoService = associadoService;         
        }

        [HttpGet("[action]/{codigoAssociado}")]
        [ProducesResponseType(typeof(Entities.Associado), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Entities.Associado>> GetAssociado(int codigoAssociado)
        {
            var associado = await _associadoService.GetAssociadoByCodigo(codigoAssociado);

            return Ok(associado ?? new Entities.Associado());
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
        public async Task<ActionResult<bool>> AlterarPlano([FromBody] AlterarPlano alterarPlano)
        {
            var result = await _associadoService.AlterarPlano(alterarPlano);

            if (!result)
                return BadRequest(new { Message = "Erro ao alterar plano" });

            return Ok(result);
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> SolicitarMarcacaoExame([FromBody] AutorizacaoExame autorizacaoExameMsg)
        {
            return Ok(await _associadoService.SolicitarMarcacaoExame(autorizacaoExameMsg));
        }
    }
}
