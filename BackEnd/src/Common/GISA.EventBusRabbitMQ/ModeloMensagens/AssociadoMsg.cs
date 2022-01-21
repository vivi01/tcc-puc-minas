using System;

namespace GISA.EventBusRabbitMQ.ModeloMensagens
{
    public class AssociadoMsg
    {
        public Guid RequestId { get; set; }

        public int CodigoAssociado { get; set; }

        public string Status { get; set; }

        public string Token { get; set; }
    }
}
