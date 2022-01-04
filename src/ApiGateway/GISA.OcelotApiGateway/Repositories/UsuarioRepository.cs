using GISA.OcelotApiGateway.Data.Interfaces;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.Security;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IAuthContext _context;

        public UsuarioRepository(IAuthContext context)
        {
            _context = context;
        }

        public async Task Create(AuthUser usuario)
        {
            await _context.Usuarios.InsertOneAsync(usuario);
        }

        public async Task<bool> Delete(string id)
        {
            var filter = Builders<AuthUser>.Filter.Eq(p => p.Id, id);

            var deleteResult = await _context
                .Usuarios
                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                   && deleteResult.DeletedCount > 0;
        }

        public async Task<AuthUser> GetUsuarioById(string id)
        {
            return await _context.Usuarios
                 .Find(p => p.Id == id)
                 .FirstOrDefaultAsync();
        }

        public async Task<AuthUser> GetUsuarioByName(string userName)
        {
            var filter = Builders<AuthUser>.Filter.Eq(p => p.Username, userName);

            return await _context.Usuarios
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AuthUser>> GetUsuarios()
        {
            return await _context.Usuarios
               .Find(p => true)
               .ToListAsync();
        }

        public async Task<bool> Update(AuthUser usuario)
        {
            var updateResult = await _context
               .Usuarios
               .ReplaceOneAsync(filter: g => g.Id == usuario.Id, replacement: usuario);

            return updateResult.IsAcknowledged
                   && updateResult.ModifiedCount > 0;
        }
    }
}
