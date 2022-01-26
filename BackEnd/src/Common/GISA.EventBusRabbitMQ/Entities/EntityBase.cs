using System;

namespace GISA.EventBusRabbitMQ.Entities
{
    public abstract class EntityBase
    {
        public Guid Id { get; set; }
        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}
