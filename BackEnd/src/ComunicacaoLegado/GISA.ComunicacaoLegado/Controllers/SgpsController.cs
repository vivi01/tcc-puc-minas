using GISA.ComunicacaoLegado.Services;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GISA.ComunicacaoLegado.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SgpsController : ControllerBase
    {
        private readonly ISgpsService _sgpsService;

        public SgpsController(ISgpsService sgpsService)
        {
            _sgpsService = sgpsService;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(DefaultResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<DefaultResponse>> AutorizarExame([FromBody] AutorizacaoExameMsg autorizacaoExameMsg)
        {
            return Ok(_sgpsService.AutorizarExame(autorizacaoExameMsg));
        }
    }
}
