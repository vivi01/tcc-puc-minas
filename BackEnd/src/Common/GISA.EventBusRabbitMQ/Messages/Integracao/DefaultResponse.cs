using System.Collections.Generic;

namespace GISA.EventBusRabbitMQ.Messages.Integracao
{
    public class DefaultResponse
    {
        public string Title { get; set; }
        public string Status { get; set; }        
        public bool Sucess { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
