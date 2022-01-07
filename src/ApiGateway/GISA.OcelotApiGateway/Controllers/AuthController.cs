using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<AuthToken>> GetAuthentication([FromBody] AuthUser user)
        {
            if (!_usuarioService.ValidarUsuario(user))
            {
                return BadRequest(new { Message = "Email e/ou senha está(ão) inválido(s)." });
            }

            var token = await _tokenService.GetTokenByUserName(user.Username);

            if (token != null)
            {
                return await _tokenService.ValidarToken(user, token);
            }

            return await _tokenService.GerarNovoToken(user);
        }
    }
}
