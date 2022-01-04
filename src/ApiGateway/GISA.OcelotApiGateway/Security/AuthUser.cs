using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GISA.OcelotApiGateway.Security
{
    public class AuthUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }
       
        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
