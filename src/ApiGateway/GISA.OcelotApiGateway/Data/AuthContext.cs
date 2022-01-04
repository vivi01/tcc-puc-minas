using GISA.OcelotApiGateway.Data.Interfaces;
using GISA.OcelotApiGateway.Security;
using GISA.OcelotApiGateway.Settings;
using MongoDB.Driver;

namespace GISA.OcelotApiGateway.Data
{
    public class AuthContext : IAuthContext
    {
        public IMongoCollection<AuthUser> Usuarios { get; }

        public IMongoCollection<AuthToken> Tokens { get; }

        public AuthContext(IAuthDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            Usuarios = database.GetCollection<AuthUser>(settings.UsuarioCollectionName);

            Tokens = database.GetCollection<AuthToken>(settings.TokenCollectionName);

            AuthContextSeed.SeedData(Usuarios);
        }
    }
}
