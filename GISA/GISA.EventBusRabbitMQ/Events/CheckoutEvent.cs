using System;

namespace GISA.EventBusRabbitMQ.Events
{
    //TODO renomear para um nome mais genérico
    public class CheckoutEvent
    {
        public Guid RequestId { get; set; }
    }
}
