using EasyNetQ;
using GISA.EventBusRabbitMQ.Entities;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using System;
using System.Threading.Tasks;

namespace GISA.EventBusRabbitMQ.Interfaces
{
    public interface IMessageBus : IDisposable
    {
        bool IsConnected { get; }
        IAdvancedBus AdvancedBus { get; }
        void Publish<T>(T message) where T : EntityBase, IAgragador;
        Task PublishAsync<T>(T message) where T : EntityBase, IAgragador;

        void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class;

        void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;

        TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : EntityBase, IAgragador
            where TResponse : AutorizacaoExameResponse;

        Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : EntityBase, IAgragador
            where TResponse : AutorizacaoExameResponse;

        IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : EntityBase, IAgragador
            where TResponse : AutorizacaoExameResponse;

        IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : EntityBase, IAgragador
            where TResponse : AutorizacaoExameResponse;
    }
}
