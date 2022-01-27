using GISA.EventBusRabbitMQ.Entities;
using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Interfaces;
using System;

namespace GISA.EventBusRabbitMQ.Messages
{
    public class AutorizacaoExameMsg : EntityBase, IAgragador
    {
        public Guid RequestId { get; set; }
        public int CodigoExame { get; set; }
        public int CodigoAssociado { get; set; }
        public EStatusSolicitacao StatusSolicitacao { get; set; }
        public string StatusAssociado { get; set; }
        public int CodigoPlano { get; set; }
        public DateTime DataExame { get; set; }        
    }
}
