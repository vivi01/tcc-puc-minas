using FluentAssertions;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using GISA.Prestador.Controllers;
using GISA.Prestador.Entities;
using GISA.Prestador.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
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
            var autorizacaoExame = new AutorizacaoExameMsg
            {
                RequestId = new Guid(),
                CodigoAssociado = 1258,
                CodigoExame = 254,
                CodigoPlano = 27,
                DataExame = new DateTime(2022, 02, 10),                
                StatusSolicitacao = "Autorizado"               
            };

            var response = new DefaultResponse
            {
                Status = "Autorizado",
                Sucess = true,
                Title = "Autorizado pelo SGPS"
            };

            _prestadorServiceMock.Setup(_ => _.SolicitarAutorizacoExame(autorizacaoExame))
                .Returns(Task.FromResult(response));

            //Act
            var actionResult = await prestadorController.SolicitarAutorizacaoExame(autorizacaoExame);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(response);
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

        [Test]
        public async Task CadastrarPrestadorComSucesso()
        {
            //Arrange
            Entities.Prestador associado = GetMockPrestador();

            _prestadorServiceMock.Setup(x => x.CadastrarPrestador(associado))
                .ReturnsAsync(true);

            //Act
            var actionResult = await prestadorController.CadastrarPrestador(associado);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(true);
        }
        
        [Test]
        public async Task CadastrarPrestadorDeveRetornarBadRequest()
        {
            //Arrange
            Entities.Prestador associado = GetMockPrestador();

            _prestadorServiceMock.Setup(x => x.CadastrarPrestador(associado))
                .ReturnsAsync(false);

            //Act
            var actionResult = await prestadorController.CadastrarPrestador(associado);

            //Assert           
            var result = actionResult.Result as BadRequestObjectResult;
            result.Should().NotBeNull();
            Assert.AreEqual(Convert.ToInt32(HttpStatusCode.BadRequest), result.StatusCode);
        }

        private Entities.Prestador GetMockPrestador()
        {
            var endereco = new Endereco
            {
                Rua = "Rua X",
                CEP = "42510698",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro"
            };

            return new Entities.Prestador
            {
                Categoria = Enums.ECategoria.Medico,
                CodigoPrestador = 256,
                CpfCnpj = "568369835614",
                DataNascimento = new DateTime(1960, 03, 05),
                Email = "prestador1@gmail.com",
                Endereco = endereco,
                Formacao = "medicina",
                Nome = "José Ribeiro",
                Planos = GetMockPlanosConveniados()
            };
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
