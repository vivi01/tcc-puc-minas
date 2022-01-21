using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services.Interfaces;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace GISA.Prestador.Services
{
    public class PrestadorService : IPrestadorService
    {
        public readonly IPrestadorRepository _prestadorRepository;
        private readonly IPlanoService _planoService;
        private readonly IModel _channel;
        private readonly IConnection _connection;

        public PrestadorService(IPrestadorRepository prestadorRepository, IPlanoService planoService)
        {
            _prestadorRepository = prestadorRepository;
            _planoService = planoService;
            var factory = new ConnectionFactory
            {
                HostName = "localhost"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }
        public async Task<string> SolicitarAutorizacoExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            if (autorizacaoExameMsg.StatusAssociado != "Ativo")
            {
                return "Nao Autorizado! Associado não está ativo";
            }

            var isConveniado = await GetPlanoConveniado(autorizacaoExameMsg.CodigoPlano);

            if (isConveniado == null)
            {
                return "Nao Autorizado! Plano não conveniado";
            }

            //chama o legado SGPS para solicitar a autorização do exame
            GetAutorizacaoExame(autorizacaoExameMsg);

            if (autorizacaoExameMsg.Status != "Autorizado")
            {
                return "Nao Autorizado";
            }

            return "Autorizado";
        }

        private void GetAutorizacaoExame(AutorizacaoExameMsg requestMessage)
        {
            var output = JsonConvert.SerializeObject(requestMessage);
            var body = Encoding.UTF8.GetBytes(output);
            _channel.BasicPublish(string.Empty, EventBusConstants.GisaQueue, null, body);
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
