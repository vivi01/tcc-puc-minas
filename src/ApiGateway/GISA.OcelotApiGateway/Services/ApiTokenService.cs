using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using GISA.OcelotApiGateway.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.Services
{
    public class ApiTokenService : IApiTokenService
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly ILogger<ApiTokenService> _logger;

        public ApiTokenService(ITokenRepository tokenRepository, ILogger<ApiTokenService> logger)
        {
            _tokenRepository = tokenRepository;
            _logger = logger;
        }

        public async Task<AuthToken> GerarNovoToken(AuthUser user)
        {
            var key = GerarChaveDeAcordoComPerfil(user.Role);
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expirationDate = DateTime.UtcNow.AddDays(7);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var token = new JwtSecurityToken(audience: $"{user.Role}Audience",
                                              issuer: $"{user.Role}Issuer",
                                              claims: claims,
                                              expires: expirationDate,
                                              signingCredentials: credentials);

            AuthToken authToken = CriarToken(user, expirationDate, token);

            await _tokenRepository.Create(authToken);

            return authToken;
        }

        public async Task<AuthToken> GetTokenByUserName(string userName)
        {
            return await _tokenRepository.GetTokenByUserName(userName);
        }

        public async Task<AuthToken> ValidarToken(AuthUser user, AuthToken token)
        {
            if (token.ExpirationDate < DateTime.Now)
            {
                _logger.LogInformation($"Token Expirado! Gerado novo token para o usuário {user.Username} ");

                var newToken = await GerarNovoToken(user);
                await _tokenRepository.Update(newToken);
                return newToken;
            }

            return token;
        }

        private AuthToken CriarToken(AuthUser user, DateTime expirationDate, JwtSecurityToken token)
        {
            _logger.LogInformation($"Gerado token para o usuário {user.Username} ");
            return new AuthToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpirationDate = expirationDate,
                UserName = user.Username
            };
        }

        private static SymmetricSecurityKey GerarChaveDeAcordoComPerfil(string role)
        {
            if (role == "associado")
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.AssociadoSecret));

            if (role == "conveniado")
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.ConveniadoSecret));

            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthSettings.PrestadorSecret));
        }
    }
}
