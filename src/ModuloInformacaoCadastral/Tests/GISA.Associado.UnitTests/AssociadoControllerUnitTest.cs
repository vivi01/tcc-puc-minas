using FluentAssertions;
using GISA.Associado.Controllers;
using GISA.Associado.Entities;
using GISA.Associado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.UnitTests
{
    public class Tests
    {
        AssociadoController associadoController;
        private Mock<IAssociadoService> _associadoServiceMock;

        [SetUp]
        public void Setup()
        {
            _associadoServiceMock = new Mock<IAssociadoService>();

            associadoController = new AssociadoController(_associadoServiceMock.Object);
        }

        [Test]
        public async Task GetAssociadoDeveRetornarAssociadoComSucesso()
        {
            //Arrange
            var codigoAssociado = 3005;
            Entities.Associado associado = GetMockAssociado(codigoAssociado);

            _associadoServiceMock.Setup(x => x.GetAssociado(codigoAssociado))
                .ReturnsAsync(associado);

            //Act
            var actionResult = await associadoController.GetAssociado(codigoAssociado);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(associado);
        }

        [Test]
        public async Task GetGetTodosPlanosDisponiveisDeveRetornarAssociadoComSucesso()
        {
            //Arrange
            _associadoServiceMock.Setup(x => x.GetTodosPlanosDisponiveis())
                .ReturnsAsync(GetTodosPlanosMock());

            //Act
            var actionResult = await associadoController.GetTodosPlanosDisponiveis();

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(GetTodosPlanosMock());
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