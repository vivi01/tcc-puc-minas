using GISA.ComunicacaoLegado.Services;
using GISA.EventBusRabbitMQ.Events;
using Microsoft.AspNetCore.Mvc;

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

        //[HttpPost("[action]")]
        //public IActionResult AutorizarExame([FromBody] AutorizacaoExameMsg autorizacaoExameMsg)
        //{
        //    return Ok(_sgpsService.AutorizarExame(autorizacaoExameMsg));
        //}
    }
}
