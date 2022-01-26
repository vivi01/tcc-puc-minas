using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services;
using GISA.Prestador.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.UnitTests.Services
{
    public class PrestadorServiceUnitTests
    {
        private PrestadorService prestadorService;
        private Mock<IPrestadorRepository> _prestadorRepositoryMock;
        private Mock<IPlanoService> _planoServiceMock;
        private Mock<IMessageBus> _busMock;

        [SetUp]
        public void Setup()
        {
            _prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            _planoServiceMock = new Mock<IPlanoService>();
            _busMock = new Mock<IMessageBus>();

            prestadorService = new PrestadorService(_prestadorRepositoryMock.Object, _planoServiceMock.Object,
               _busMock.Object);
        }

        [Test]
        public async Task SolicitarMarcacaoExameComSucesso()
        {
            //Arrange
            var autorizacaoExame = new AutorizacaoExameMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = 1258,
                CodigoExame = 254,
                CodigoPlano = 27,
                DataExame = new System.DateTime(2022, 02, 10),
                StatusSolicitacao = "Autorizado"
            };

            var plano = new Plano
            {
                CodigoPlano = 1257,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                Descricao = "Plano básico",
                TipoPlano = Enums.ETipoPlano.Individual
            };

            _planoServiceMock.Setup(x => x.ObterPlanoPorCodigo(autorizacaoExame.CodigoPlano))
                .ReturnsAsync(plano);

            //Act
            var result = await prestadorService.SolicitarAutorizacoExame(autorizacaoExame);

            //Assert           
            result.Equals("Autorizado");
        }


        [Test]
        public async Task GetPlanoConveniadoByCodigoComSucesso()
        {
            //Arrange
            var codigoPlano = 1257;
            var plano = new Plano
            {
                CodigoPlano = 1257,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                Descricao = "Plano básico",
                TipoPlano = Enums.ETipoPlano.Individual
            };

            _planoServiceMock.Setup(x => x.ObterPlanoPorCodigo(codigoPlano))
                .ReturnsAsync(plano);

            //Act
            var result = await prestadorService.GetPlanoConveniado(codigoPlano);

            //Assert           
            result.Equals(true);
        }

        [Test]
        public async Task GetAllPlanosConveniadosComSucesso()
        {
            //Arrange
            var planos = GetMockPlanosConveniados();

            _planoServiceMock.Setup(x => x.ObterTodos())
                .ReturnsAsync(planos);

            //Act
            var result = await prestadorService.GetAllPlanosConveniados();

            //Assert           
            result.Equals(planos);
        }

        private static List<Entities.Plano> GetMockPlanosConveniados()
        {
            var planos = new List<Plano>();
            planos.Add(new Plano
            {
                CodigoPlano = 20,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                Descricao = "Plano básico",
                TipoPlano = Enums.ETipoPlano.Individual
            });

            planos.Add(new Plano
            {
                CodigoPlano = 25,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Apartamento,
                Descricao = "Plano intermediario",
                TipoPlano = Enums.ETipoPlano.Individual
            });

            planos.Add(new Plano
            {
                CodigoPlano = 30,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Vip,
                Descricao = "Plano VIP",
                TipoPlano = Enums.ETipoPlano.Empresarial
            });

            return planos;
        }

    }
}
