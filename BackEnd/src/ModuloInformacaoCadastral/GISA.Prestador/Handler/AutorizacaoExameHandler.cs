using GISA.Prestador.EventsHandlers;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GISA.Prestador.Command
{
    public class AutorizacaoExameHandler : LogEventHandler, IRequestHandler<AutorizacaoExameCommand, string>
    {
        private readonly IMediator _mediator;
        public AutorizacaoExameHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<string> Handle(AutorizacaoExameCommand request, CancellationToken cancellationToken)
        {
            request.Status = "Autorizado";

            try
            {
                var notification = new AutorizacaoExameNotification
                {
                    RequestId = new Guid(),
                    CodigoAssociado = request.CodigoAssociado,
                    CodigoExame = request.CodigoExame,
                    DataExame = request.DataExame,
                    Status = "Autorizado"
                };
                await _mediator.Publish(notification);
                return await Task.FromResult("Autorizado");
            }
            catch (Exception ex)
            {
                var notification = new AutorizacaoExameNotification { 
                                                                         RequestId = new Guid(), 
                                                                        CodigoAssociado = request.CodigoAssociado,
                                                                        CodigoExame = request.CodigoExame, 
                                                                        DataExame = request.DataExame, 
                                                                        Status = request.Status };

                await _mediator.Publish(notification);
                await _mediator.Publish(new ErroNotification { Erro = ex.Message, PilhaErro = ex.StackTrace });
                return await Task.FromResult("Ocorreu um erro no momento da solicitação");
            }
        }
    }
}
