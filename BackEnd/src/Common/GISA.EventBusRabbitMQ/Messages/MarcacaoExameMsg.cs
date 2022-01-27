using GISA.EventBusRabbitMQ.Entities;
using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Interfaces;
using System;

namespace GISA.EventBusRabbitMQ.Messages
{
    public class MarcacaoExameMsg : EntityBase, IAgragador
    {
        public int CodigoExame { get; set; }
        public int CodigoAssociado { get; set; }
        public int CodigoPlano { get; set; }
        public EStatusSolicitacao Status { get; set; }
        public string StatusAssociado { get; set; }
        public DateTime DataExame { get; set; }
        public string MensagensErro { get; set; }
    }
}
