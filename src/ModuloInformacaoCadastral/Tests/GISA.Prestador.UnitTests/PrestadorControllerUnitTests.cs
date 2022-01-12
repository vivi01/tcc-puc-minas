using FluentAssertions;
using GISA.EventBusRabbitMQ.Events;
using GISA.Prestador.Controllers;
using GISA.Prestador.Entities;
using GISA.Prestador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Prestador.UnitTests
{
    public class PrestadorControllerUnitTests
    {
        private PrestadorController prestadorController;
        private Mock<IPrestadorService> _prestadorServiceMock;

        [SetUp]
        public void SetUp()
        {
            _prestadorServiceMock = new Mock<IPrestadorService>();
            prestadorController = new PrestadorController(_prestadorServiceMock.Object);
        }

        [Test]
        public async Task SolicitarAutorizacoExameDeveRetornarComSucesso()
        {
            //Arrange
            string token = "kskaksjlakjdkjd5656456";

            var autorizacaoExame = new AutorizacaoExameMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = 1258,
                CodigoExame = 254,
                CodigoPlano = 27,
                DataExame = new System.DateTime(2022, 02, 10),
                MensagensErro = "",
                Status = "Autorizado",
                Token = "x14589909mlpq09875cv12"
            };

            _prestadorServiceMock.Setup(_ => _.SolicitarAutorizacoExame(token, autorizacaoExame))
                .Returns(Task.FromResult("autorizado"));

            //Act
            var actionResult = await prestadorController.SolicitarAutorizacoExame(token, autorizacaoExame);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be("autorizado");
        }

        [Test]
        public async Task GetAllPlanosConveniadosComSucesso()
        {
            //Arrange
            var planos = GetMockPlanosConveniados();

            _prestadorServiceMock.Setup(x => x.GetAllPlanosConveniados())
                .ReturnsAsync(planos);

            //Act
            var result = await prestadorController.GetAllPlanosConveniados();

            //Assert           
            result.Equals(planos);
        }

        private static List<Plano> GetMockPlanosConveniados()
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
