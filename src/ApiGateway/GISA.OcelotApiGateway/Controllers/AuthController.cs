using GISA.OcelotApiGateway.Models;
using GISA.OcelotApiGateway.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GISA.OcelotApiGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        [Route("associados")]
        [AllowAnonymous]
        public ActionResult<AuthToken> GetAssociadosAuthentication([FromBody] AuthUser user)
        {
            if (user.Username != "associado_user" || user.Password != "456")
            {
                return BadRequest(new { message = "Username or Password is invalid" });
            }

            return new AssociadosApiTokenService().GenerateToken(user);
        }

        [HttpPost]
        [Route("conveniados")]
        [AllowAnonymous]
        public ActionResult<AuthToken> GetConveniadosAuthentication([FromBody] AuthUser user)
        {
            if (user.Username != "conveniado_user" || user.Password != "123")
            {
                return BadRequest(new { message = "Username or Password is invalid" });
            }

            return new ConveniadosApiTokenService().GenerateToken(user);
        }
    }
}
