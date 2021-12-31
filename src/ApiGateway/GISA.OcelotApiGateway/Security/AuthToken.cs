using System;

namespace GISA.OcelotApiGateway.Security
{
    public class AuthToken
    {
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
