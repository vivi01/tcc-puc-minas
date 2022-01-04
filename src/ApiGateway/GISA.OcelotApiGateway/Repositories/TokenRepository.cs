using GISA.OcelotApiGateway.Data.Interfaces;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.Security;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Repositories
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IAuthContext _context;

        public TokenRepository(IAuthContext context)
        {
            _context = context;
        }

        public async Task Create(AuthToken token)
        {
            await _context.Tokens.InsertOneAsync(token);
        }       

        public async Task<AuthToken> GetTokenByUserName(string userName)
        {
            var filter = Builders<AuthToken>.Filter.Eq(p => p.UserName, userName);

            return await _context.Tokens
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<bool> Update(AuthToken token)
        {
            var updateResult = await _context
                .Tokens
                .ReplaceOneAsync(filter: g => g.Id == token.Id, replacement: token);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }
    }
}
