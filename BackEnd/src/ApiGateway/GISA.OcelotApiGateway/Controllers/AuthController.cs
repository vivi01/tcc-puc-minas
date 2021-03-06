using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUsuarioService _usuarioService;

        public AuthController(ITokenService tokenService, IUsuarioService usuarioService)
        {
            _tokenService = tokenService;
            _usuarioService = usuarioService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<AuthToken>> GetAuthentication([FromBody] AuthUser user)
        {
            if (!_usuarioService.ValidarUsuario(user))
            {
                return BadRequest(new { Message = "Email e/ou senha está(ão) inválido(s)." });
            }

            var token = await _tokenService.GetTokenByUserName(user.Username);

            if (token != null)
            {
                return Ok(await _tokenService.ValidarToken(user, token));
            }

            return Ok(await _tokenService.GerarNovoToken(user));
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        [ProducesResponseType(typeof(AuthToken), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<AuthToken>> GetUsuario(string token)
        {
            return Ok( await _tokenService.GetTokenByToken(token));
        }
    }
}
