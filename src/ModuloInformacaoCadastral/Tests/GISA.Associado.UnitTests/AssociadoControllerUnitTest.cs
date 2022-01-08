using FluentAssertions;
using GISA.Associado.Controllers;
using GISA.Associado.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
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
            var endereco = new Entities.Endereco
            {
                Rua = "Rua X",
                CEP = "42510698",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro"
            };

            var plano = new Entities.Plano
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
                MarcacaoExames = new System.Collections.Generic.List<Entities.MarcacaoExame>()
            };

            _associadoServiceMock.Setup(x => x.GetAssociado(codigoAssociado))
                .ReturnsAsync(associado);

            //Act
            var actionResult = await associadoController.GetAssociado(codigoAssociado);

            //Assert           
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(associado);
        }
    }
}