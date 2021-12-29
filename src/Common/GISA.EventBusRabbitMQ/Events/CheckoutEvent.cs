using System;

namespace GISA.EventBusRabbitMQ.Events
{
    public class CheckoutEvent
    {
        public Guid RequestId { get; set; }
    }
}
