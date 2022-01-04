using GISA.OcelotApiGateway.Security;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        Task<AuthToken> GetTokenByUserName(string userName);

        Task Create(AuthToken token);

        Task<bool> Update(AuthToken token);       
    }
}
