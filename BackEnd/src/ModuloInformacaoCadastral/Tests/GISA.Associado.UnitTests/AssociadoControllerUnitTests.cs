using FluentAssertions;
using GISA.Associado.Controllers;
using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace GISA.Associado.UnitTests
{
    public class Tests
    {
        AssociadoController associadoController;
        private Mock<IAssociadoService> _associadoServiceMock;
        private Mock<IPlanoService> _planoServiceMock;

        [SetUp]
        public void Setup()
        {
            _associadoServiceMock = new Mock<IAssociadoService>();
            _planoServiceMock = new Mock<IPlanoService>();

            associadoController = new AssociadoController(_associadoServiceMock.Object);
        }

        [Test]
        public async Task GetAssociadoDeveRetornarAssociadoComSucesso()
        {
            //Arrange
            var codigoAssociado = 3005;
            Entities.Associado associado = GetMockAssociado(codigoAssociado);

            _associadoServiceMock.Setup(x => x.GetAssociadoByCodigo(codigoAssociado))
                .ReturnsAsync(associado);

            //Act
            var actionResult = await associadoController.GetAssociado(codigoAssociado);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(associado);
        }       

        [Test]
        public async Task AlterarPlanoRetornarAssociadoComSucesso()
        {
            //Arrange

            var codigoAssociado = 156;
            var codigoNovoPlano = 27;
            var planoOdonlogico = false;

            var alterarPlano = new AlterarPlano
            {
                CodigoAssociado = codigoAssociado,
                CodigoNovoPlano = codigoNovoPlano,
                PlanoOdontologico = planoOdonlogico
            };

            _associadoServiceMock.Setup(x => x.AlterarPlano(alterarPlano))
                .ReturnsAsync(true);

            //Act
            var actionResult = await associadoController.AlterarPlano(alterarPlano);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(true);
        }

        [Test]
        public async Task SolicitarMarcacaoExameComSucesso()
        {
            //Arrange
            var autorizacaoExame = new AutorizacaoExame
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

            _associadoServiceMock.Setup(x => x.SolicitarMarcacaoExame(autorizacaoExame))
                .ReturnsAsync("Autorizado");

            //Act
            var actionResult = await associadoController.SolicitarMarcacaoExame(autorizacaoExame);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be("Autorizado");
        }

        [Test]
        public async Task CadastrarAssociadoComSucesso()
        {
            //Arrange
            var codigoAssociado = 3005;
            Entities.Associado associado = GetMockAssociado(codigoAssociado);

            _associadoServiceMock.Setup(x => x.CadastrarAssociado(associado))
                .ReturnsAsync(true);

            //Act
            var actionResult = await associadoController.CadastrarAssociado(associado);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(true);
        }

        [Test]
        public async Task CadastrarAssociadoDeveRetornarBadRequest()
        {
            //Arrange
            var codigoAssociado = 3005;
            Entities.Associado associado = GetMockAssociado(codigoAssociado);

            _associadoServiceMock.Setup(x => x.CadastrarAssociado(associado))
                .ReturnsAsync(false);

            //Act
            var actionResult = await associadoController.CadastrarAssociado(associado);

            //Assert           
            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            Assert.AreEqual(Convert.ToInt32(HttpStatusCode.BadRequest), result.StatusCode);
        }

        private static Entities.Associado GetMockAssociado(int codigoAssociado)
        {
            var endereco = new Endereco
            {
                Rua = "Rua X",
                CEP = "42510698",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro"
            };

            var plano = new Plano
            {
                CodigoPlano = 25,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                Descricao = "Plano básico",
                TipoPlano = Enums.ETipoPlano.Individual
            };
            var associado = new Entities.Associado
            {
                Nome = "José Maria",
                DataNascimento = new System.DateTime(1960, 7, 15),
                CpfCnpj = "04598637411",
                CodigoAssociado = codigoAssociado,
                Endereco = endereco,
                Formacao = "",
                Plano = plano,
                ValorPlano = 800,
                Email = "joseMaria@teste.com",
                PossuiPlanoOdontologico = false,
                MarcacaoExames = new List<MarcacaoExame>()
            };
            return associado;
        }

        private static List<Plano> GetTodosPlanosMock()
        {
            return new List<Plano>
            {
               new Plano  {
                   CodigoPlano = 25,
                   ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                   Descricao = "Plano básico",
                   TipoPlano = Enums.ETipoPlano.Individual
               },
               new Plano  {
                   CodigoPlano = 26,
                   ClassificacaoPlano = Enums.EClassificacaoPlano.Apartamento,
                   Descricao = "Plano básico",
                   TipoPlano = Enums.ETipoPlano.Individual
               },
                new Plano  {
                   CodigoPlano = 27,
                   ClassificacaoPlano = Enums.EClassificacaoPlano.Vip,
                   Descricao = "Plano Top",
                   TipoPlano = Enums.ETipoPlano.Individual
               },
               new Plano  {
                   CodigoPlano = 28,
                   ClassificacaoPlano = Enums.EClassificacaoPlano.Apartamento,
                   Descricao = "Plano intermediário",
                   TipoPlano = Enums.ETipoPlano.Empresarial
               }
            };
        }
    }
}