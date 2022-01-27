using GISA.EventBusRabbitMQ.Enums;
using System;

namespace GISA.EventBusRabbitMQ.Messages.Integracao
{
    public class MarcacaoExameResponse
    {
        public EStatusSolicitacao Situacao { get; set; }
        public int CodigoExame { get; set; }
        public DateTime DataExame { get; set; }
        public DateTime? DataAutorizacao { get; set; }
        public string MensagemErro { get; set; }
    }
}
