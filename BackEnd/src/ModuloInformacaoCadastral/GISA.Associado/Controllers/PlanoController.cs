using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plano>>> Get()
        {
            var result = await _planoService.ObterTodos();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Plano>> Get(int id)
        {
            var result = await _planoService.ObterPlanoPorId(id);

            if (result == null)
                return BadRequest(new { Message = "Erro ao buscar um Plano" });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Plano plano)
        {
            var result = await _planoService.Adicionar(plano);

            if (!result)
                return BadRequest(new { Message = "Erro ao realizar Cadastro" });

            return Ok("Plano cadastrado com Sucesso");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Plano plano)
        {
            if (plano.Id != id)
            {
                return BadRequest($"Não foi possivel atualizar o plano com id={id}");
            }

            var result = await _planoService.Editar(plano);

            if (!result)
                return BadRequest(new { Message = "Erro ao alterar plano" });

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var plano = await _planoService.ObterPlanoPorId(id);
            var result = await _planoService.Deletar(plano);

            if (!result)
                return BadRequest(new { Message = "Erro ao alterar plano" });

            return Ok("Plano deletado com Sucesso");
        }
    }
}
