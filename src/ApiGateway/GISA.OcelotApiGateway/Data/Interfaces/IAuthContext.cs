using GISA.OcelotApiGateway.Security;
using MongoDB.Driver;

namespace GISA.OcelotApiGateway.Data.Interfaces
{
    public interface IAuthContext
    {
        IMongoCollection<AuthUser> Usuarios { get; }

        IMongoCollection<AuthToken> Tokens { get; }
    }
}
