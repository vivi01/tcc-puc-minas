using MediatR;
using System;

namespace GISA.Prestador.Command
{
    public class AutorizacaoExameNotification : INotification
    {
        public Guid RequestId { get; set; }

        public int CodigoExame { get; set; }

        public int CodigoAssociado { get; set; }
     
        public string Status { get; set; }      

        public DateTime DataExame { get; set; }
       
    }
}
