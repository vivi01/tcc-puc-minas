using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PlanoController : ControllerBase
    {
        private readonly IPlanoService _planoService;

        public PlanoController(IPlanoService planoService)
        {
            _planoService = planoService;
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Plano>>> GetTodosPlanos()
        {
            var result = await _planoService.ObterTodos();

            return Ok(result);
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<Plano>> Get(int id)
        {
            var result = await _planoService.ObterPlanoPorId(id);

            if (result == null)
                return BadRequest(new { Message = "Erro ao buscar um Plano" });

            return Ok(result);
        }       
    }
}
