using System;

namespace GISA.EventBusRabbitMQ.Events
{
    public class AutorizacaoExameMsg
    {
        public Guid RequestId { get; set; }
       
        public int CodigoExame { get; set; }

        public int CodigoAssociado { get; set; }

        public int CodigoPlano { get; set; }

        public string Status { get; set; }

        public string StatusAssociado { get; set; }

        public string Token { get; set; }

        public DateTime DataExame { get; set; }

        public string MensagensErro { get; set; }
    }
}
