using GISA.OcelotApiGateway.Security;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services
{
    public interface IApiTokenService
    {
        Task<AuthToken> GenerateToken(AuthUser user);

        Task<AuthToken> ValidarToken(AuthUser user, AuthToken token);

        Task<AuthToken> GetTokenByUserName(string userName);
    }
}
