using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using GISA.Prestador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PrestadorController : ControllerBase
    {
        private IPrestadorService _prestadorService;

        public PrestadorController(IPrestadorService prestadorService)
        {
            _prestadorService = prestadorService;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SolicitarAutorizacoExame(string token, AutorizacaoExameMsg autorizacaoExameMsg)
        {
            return Ok(await _prestadorService.SolicitarAutorizacoExame(token, autorizacaoExameMsg));
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Plano>>> GetAllPlanosConveniados()
        {
            var planos = await _prestadorService.GetAllPlanosConveniados();

            return Ok(planos ?? new List<Plano>());
        }



    }
}
