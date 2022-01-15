using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public readonly IPrestadorRepository _prestadorRepository;
        private readonly IPlanoService _planoService;
        private readonly IBus _busControl;

        public PrestadorService(IPrestadorRepository prestadorRepository, IBus busControl, IPlanoService planoService)
        {
            _prestadorRepository = prestadorRepository;
            _busControl = busControl;
            _planoService = planoService;
        }
        public async Task<string> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            var requestMessage = new AssociadoMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = autorizacaoExameMsg.CodigoAssociado,
                Token = autorizacaoExameMsg.Token
            };

            //chama o microsserviço do associado para verificar o status
            GetSituacaoAssociado(requestMessage);

            if (requestMessage.Status != "Ativo")
            {
                await _busControl.ReceiveAsync<AssociadoMsg>(EventBusConstants.GisaQueue,
                     x =>
                     {
                         autorizacaoExameMsg.Status = "Nao Autorizado";
                         autorizacaoExameMsg.MensagensErro = "Não foi possível autorizar o exame";
                     });

                return "Nao Autorizado";
            }

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado == null)
            {
                await _busControl.ReceiveAsync<AssociadoMsg>(EventBusConstants.GisaQueue,
                    x =>
                    {
                        autorizacaoExameMsg.Status = "Nao Autorizado";
                        autorizacaoExameMsg.MensagensErro = "Plano não conveniado";
                    });

                return "Plano não conveniado";
            }

            //chama o legado SGPS para solicitar a autorização do exame

            GetAutorizacaoExame(autorizacaoExameMsg);

            if (autorizacaoExameMsg.Status != "Autorizado")
            {
                await _busControl.ReceiveAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue,
                   x =>
                   {
                       autorizacaoExameMsg.Status = "Nao Autorizado";
                       autorizacaoExameMsg.MensagensErro = "Não foi possível autorizar o exame";
                   });

                return "Nao Autorizado";
            }

            await _busControl.ReceiveAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue,
                    x =>
                   {
                       autorizacaoExameMsg.Status = "Autorizado";
                   });
            return "Autorizado";
        }

        private async void GetSituacaoAssociado(AssociadoMsg requestMessage)
        {
            await _busControl.SendAsync<AssociadoMsg>(EventBusConstants.GisaQueue, requestMessage);
        }
        private async void GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue, requestMessage);
        }

        public async Task<bool> CadastrarPrestador(Entities.Prestador prestador)
        {
            return await _prestadorRepository.Add(prestador);
        }

        public async Task<Plano> GetPlanoConveniado(int codigoPlano)
        {
            return await _planoService.ObterPlanoPorCodigo(codigoPlano);
        }

        public async Task<List<Plano>> GetAllPlanosConveniados()
        {
            return await _planoService.ObterTodos();
        }
    }
}
