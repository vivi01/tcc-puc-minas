using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace GISA.OcelotApiGateway.SecurityModel
{
    public class AuthToken
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string UserName { get; set; }
    }
}
