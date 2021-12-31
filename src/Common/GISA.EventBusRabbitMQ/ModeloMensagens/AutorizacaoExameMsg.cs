using System;

namespace GISA.EventBusRabbitMQ.Events
{
    public class AutorizacaoExameMsg
    {
        public Guid RequestId { get; set; }
       
        public int CodigoExame { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }
    }
}
