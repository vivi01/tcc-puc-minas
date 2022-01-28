using GISA.EventBusRabbitMQ.Enums;
using System;
using System.Collections.Generic;

namespace GISA.EventBusRabbitMQ.Messages.Integracao
{
    public class AutorizacaoExameResponse
    {
        public string Mensagem { get; set; }
        public EStatusSolicitacao Status { get; set; }
        public bool Sucesso { get; set; }
        public DateTime DataAutorizacao { get; set; }
        public List<string> Erros { get; set; } = new List<string>();
    }
}
