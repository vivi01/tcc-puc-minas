using System;

namespace GISA.EventBusRabbitMQ.Entities
{
    public abstract class EntityBase
    {
        public Guid RequestId { get; set; }
        protected EntityBase()
        {
            RequestId = Guid.NewGuid();
        }
    }
}
