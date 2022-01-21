using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using GISA.OcelotApiGateway.Settings;
using Microsoft.AspNetCore.DataProtection;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IDataProtectionProvider _rootProvider;
        private readonly IAuthDatabaseSettings _settings;

        public UsuarioService(IUsuarioRepository usuarioRepository, IDataProtectionProvider rootProvider, IAuthDatabaseSettings settings)
        {
            _usuarioRepository = usuarioRepository;
            _rootProvider = rootProvider;
            _settings = settings;
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

            IDataProtector protector = _rootProvider.CreateProtector(_settings.KeyDataProvider);

            if (DesProtegerSenha(protector, usuario.Password) != user.Password || usuario.Username != user.Username)
                return false;

            return true;
        }

        private string DesProtegerSenha(IDataProtector protector, string senha)
        {
            return protector.Unprotect(senha);
        }
    }
}
