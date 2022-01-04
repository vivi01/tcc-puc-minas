using GISA.OcelotApiGateway.SecurityModel;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace GISA.OcelotApiGateway.Data
{
    public class AuthContextSeed
    {
        private readonly IDataProtector _protector;

        public AuthContextSeed(IDataProtector protector)
        {
            _protector = protector;
        }

        public void SeedData(IMongoCollection<AuthUser> usuarioCollection)
        {
            var usuarioExiste = usuarioCollection.Find(p => true).Any();

            if (!usuarioExiste)
            {
                usuarioCollection.InsertManyAsync(GetUsuariosPreConfigurados());
            }
        }

        private IEnumerable<AuthUser> GetUsuariosPreConfigurados() => new List<AuthUser>
            {
                new AuthUser
                {
                    Username = "vivi",
                    Password = ProtegerSenha("cap@2025"),
                    Role = "associado"
                },
                new AuthUser
                {
                    Username = "nando",
                    Password = ProtegerSenha("test@105"),
                    Role = "prestador"
                },
                new AuthUser
                {
                    Username = "tccpuc",
                    Password = ProtegerSenha("xrt@1258"),
                    Role = "conveniado"
                },                
                new AuthUser
                {
                    Username = "jose",
                    Password = ProtegerSenha("wyrt@1963"),
                    Role = "conveniado"
                }
            };

        private string ProtegerSenha(string senha)
        {
            return _protector.Protect(senha);
        }
    }
}
