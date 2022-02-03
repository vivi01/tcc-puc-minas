using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using Microsoft.AspNetCore.Mvc;
using System;
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
        [ProducesResponseType(typeof(Entities.Associado), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Entities.Associado>> GetAssociado(int codigoAssociado)
        {
            var associado = await _associadoService.GetAssociadoByCodigo(codigoAssociado);

            if(associado == null)
                return BadRequest(new { Message = "Erro ao tentar buscar associado" });

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
        [ProducesResponseType(typeof(MarcacaoExameResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<MarcacaoExameResponse>> SolicitarMarcacaoExame([FromBody] MarcacaoExameMsg marcacaoExameMsg)
        {
            return Ok(await _associadoService.SolicitarMarcacaoExame(marcacaoExameMsg));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
        public ActionResult<decimal> GetNovoValorPlano(DateTime dataNascimento, Plano plano, bool planoOdontologico)
        {
            return Ok(_associadoService.CalcularValorNovoPlano(dataNascimento, plano, planoOdontologico));
        }
    }
}
