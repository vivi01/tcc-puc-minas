using GISA.OcelotApiGateway.Security;
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
            if (user.Role != "associado")
            {
                return Unauthorized(new { message = "Acesso não autorizado" });
            }

            return new AssociadosApiTokenService().GenerateToken(user);
        }

        [HttpPost]
        [Route("prestadores")]
        [AllowAnonymous]
        public ActionResult<AuthToken> GetConveniadosAuthentication([FromBody] AuthUser user)
        {
            if (user.Role != "prestador")
            {
                return Unauthorized(new { message = "Acesso não autorizado" });
            }

            return new PrestadoresApiTokenService().GenerateToken(user);
        }

        [HttpPost]
        [Route("conveniados")]
        [AllowAnonymous]
        public ActionResult<AuthToken> GetComunicacaoAuthentication([FromBody] AuthUser user)
        {
            if (user.Role != "conveniado" || user.Role != "prestador")
            {
                return Unauthorized(new { message = "Acesso não autorizado" });
            }

            return new PrestadoresApiTokenService().GenerateToken(user);
        }
    }
}
