using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.Prestador.Entities;
using GISA.Prestador.Repositories.Interfaces;
using GISA.Prestador.Services;
using GISA.Prestador.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISA.Prestador.UnitTests.Services
{
    public class PrestadorServiceUnitTests
    {
        private PrestadorService prestadorService;
        private Mock<IPrestadorRepository> _prestadorRepositoryMock;
        private Mock<IPlanoService> _planoServiceMock;
        private Mock<IBus> _busControlMock;

        [SetUp]
        public void Setup()
        {
            _prestadorRepositoryMock = new Mock<IPrestadorRepository>();
            _planoServiceMock = new Mock<IPlanoService>();
            _busControlMock = new Mock<IBus>();

            prestadorService = new PrestadorService(_prestadorRepositoryMock.Object,
                _busControlMock.Object, _planoServiceMock.Object);
        }

        [Test]
        public async Task SolicitarMarcacaoExameComSucesso(string token, AutorizacaoExameMsg autorizacaoExameMsg)
        {
            //Arrange
            _prestadorRepositoryMock.Setup(x => x.SolicitarAutorizacoExame(token, autorizacaoExameMsg))
                .Returns(Task.FromResult("Autorizado"));

            //Act
            var result = await prestadorService.SolicitarAutorizacoExame(token, autorizacaoExameMsg);

            //Assert           
            result.Equals("Autorizado");
        }


        [Test]
        public async Task GetPlanoConveniadoByCodigoComSucesso()
        {
            //Arrange
            var codigoPlano = 1257;

            _prestadorRepositoryMock.Setup(x => x.GetPlanoConveniado(codigoPlano))
                .ReturnsAsync(true);

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
            _prestadorRepositoryMock.Setup(x => x.GetAllPlanosConveniados())
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
