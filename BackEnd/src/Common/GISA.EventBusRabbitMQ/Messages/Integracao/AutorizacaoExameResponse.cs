using GISA.EventBusRabbitMQ.Enums;
using System;
using System.Collections.Generic;

namespace GISA.EventBusRabbitMQ.Messages.Integracao
{
    public class AutorizacaoExameResponse
    {
        public string Title { get; set; }
        public EStatusSolicitacao Status { get; set; }        
        public bool Sucess { get; set; }
        public DateTime DataAutorizacao { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
