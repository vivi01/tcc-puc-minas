using GISA.OcelotApiGateway.SecurityModel;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services.Interfaces
{
    public interface IUsuarioService
    {
        Task<AuthUser> GetUsuarioById(string id);
        Task<AuthUser> GetUsuarioByUserName(string userName);
        bool ValidarUsuario(AuthUser user);
    }
}
