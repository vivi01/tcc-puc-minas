using GISA.Prestador.Command;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.Prestador.EventsHandlers
{
    public class LogEventHandler :
                            INotificationHandler<AutorizacaoExameNotification>,                           
                            INotificationHandler<ErroNotification>
    {
        public Task Handle(AutorizacaoExameNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"CRIACAO: '{notification.RequestId} " +
                    $"- {notification.CodigoAssociado} - {notification.CodigoExame}'");
            });
        }       

        public Task Handle(ErroNotification notification, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"ERRO: '{notification.Erro} \n {notification.PilhaErro}'");
            });
        }
    }
}
