using GISA.OcelotApiGateway.Security;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Repositories.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<AuthUser>> GetUsuarios();

        Task<AuthUser> GetUsuarioById(string id);

        Task<AuthUser> GetUsuarioByName(string userName);

        Task Create(AuthUser usuario);

        Task<bool> Update(AuthUser usuario);

        Task<bool> Delete(string id);
    }
}
