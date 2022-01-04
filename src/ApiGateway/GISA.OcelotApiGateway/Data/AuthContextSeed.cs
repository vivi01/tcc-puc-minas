using GISA.OcelotApiGateway.Security;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace GISA.OcelotApiGateway.Data
{
    public class AuthContextSeed
    {
        public static void SeedData(IMongoCollection<AuthUser> usuarioCollection)
        {
            var existProduct = usuarioCollection.Find(p => true).Any();

            if (!existProduct)
            {
                usuarioCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<AuthUser> GetPreconfiguredProducts()
        {
            return new List<AuthUser>
            {
                new AuthUser
                {
                    Username = "vivi",
                    Password = "cap@2025",
                    Role = "associado"
                },
                new AuthUser
                {
                    Username = "nando",
                    Password = "test@105",
                    Role = "prestador"
                },
                new AuthUser
                {
                    Username = "tccpuc",
                    Password = "xrt@1258",
                    Role = "conveniado"
                }
            };
        }
    }
}
