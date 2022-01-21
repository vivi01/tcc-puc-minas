using GISA.ComunicacaoLegado.Services;
using GISA.EventBusRabbitMQ.Events;
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public IActionResult AutorizarExame([FromBody] AutorizacaoExameMsg autorizacaoExameMsg)
        {
            _sgpsService.AutorizarExame(autorizacaoExameMsg);
            return Ok();
        }
    }
}
