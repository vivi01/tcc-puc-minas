using GISA.OcelotApiGateway.SecurityModel;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services.Interfaces
{
    public interface ITokenService
    {
        Task<AuthToken> GerarNovoToken(AuthUser user);

        Task<AuthToken> ValidarToken(AuthUser user, AuthToken token);

        Task<AuthToken> GetTokenByUserName(string userName);
    }
}
