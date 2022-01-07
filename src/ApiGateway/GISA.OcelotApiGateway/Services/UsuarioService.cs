using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Task<AuthUser> GetUsuarioById(string id)
        {
            return _usuarioRepository.GetUsuarioById(id);
        }

        public Task<AuthUser> GetUsuarioByUserName(string userName)
        {
            return _usuarioRepository.GetUsuarioByName(userName);
        }

        public bool ValidarUsuario(AuthUser user)
        {
            var usuario = GetUsuarioByUserName(user.Username).Result;

            if (usuario == null)
                return false;

            if (usuario.Password != user.Password || usuario.Username != user.Username)
                return false;

            return true;
        }
    }
}
