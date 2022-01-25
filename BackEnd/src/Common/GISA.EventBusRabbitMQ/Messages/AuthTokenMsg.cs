namespace GISA.EventBusRabbitMQ.Messages
{
    public class AuthTokenMsg
    {
        public string Token { get; set; }

        public string UserName { get; set; }

        public AuthTokenMsg(string token)
        {
            this.Token = token;
        }       
    }
}
