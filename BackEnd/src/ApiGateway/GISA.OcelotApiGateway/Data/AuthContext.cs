using GISA.OcelotApiGateway.Data.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Settings;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Driver;

namespace GISA.OcelotApiGateway.Data
{
    public class AuthContext : IAuthContext
    {
        public IMongoCollection<AuthUser> Usuarios { get; }

        public IMongoCollection<AuthToken> Tokens { get; }

        private readonly IDataProtectionProvider _rootProvider;

        public AuthContext(IAuthDatabaseSettings settings, IDataProtectionProvider rootProvider)
        {
            var client = new MongoClient(settings.ConnectionString);

            var database = client.GetDatabase(settings.DatabaseName);

            Usuarios = database.GetCollection<AuthUser>(settings.UsuarioCollectionName);

            Tokens = database.GetCollection<AuthToken>(settings.TokenCollectionName);

            _rootProvider = rootProvider;

            IDataProtector protector = _rootProvider.CreateProtector(settings.KeyDataProvider);

            new AuthContextSeed(protector).SeedData(Usuarios);
        }
    }
}
