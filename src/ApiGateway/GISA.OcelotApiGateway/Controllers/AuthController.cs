using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IApiTokenService _tokenService;

        public AuthController(IApiTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthToken>> GetAuthentication([FromBody] AuthUser user)
        {
            var token = await _tokenService.GetTokenByUserName(user.Username);

            if (token != null)
            {
                return await _tokenService.ValidarToken(user, token);
            }

            return await _tokenService.GerarNovoToken(user);
        }
    }
}
