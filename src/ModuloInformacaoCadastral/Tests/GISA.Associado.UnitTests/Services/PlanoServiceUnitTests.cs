using FluentAssertions;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using GISA.Associado.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GISA.Associado.UnitTests.Services
{
    public class PlanoServiceUnitTests
    {
        private IPlanoService planoService;
        private Mock<IPlanoRepository> _planoRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _planoRepositoryMock = new Mock<IPlanoRepository>();
            planoService = new PlanoService(_planoRepositoryMock.Object);
        }

        [Test]
        public async Task AdicionarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var plano = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Add(plano))
                .ReturnsAsync(true);

            //Act
            var result = await planoService.Adicionar(plano);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task EditarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Update(endereco))
                .ReturnsAsync(true);

            //Act
            var result = await planoService.Editar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ObterPorIdDeveSerExecutadoComSucesso()
        {
            //Arrange
            var planoId = 25;
            var plano = GetPlanoMock();

            _planoRepositoryMock.Setup(x => x.GetById(planoId))
                .ReturnsAsync(plano);

            //Act
            var result = await planoService.ObterPlanoPorCodigo(planoId);

            //Assert
            result.Should().Be(plano);
        }

        [Test]
        public async Task DeletarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Delete(endereco))
                .ReturnsAsync(true);

            //Act
            var result = await planoService.Deletar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ObterTodosDeveSerExecutadoComSucesso()
        {
            //Arrange
            var planos = GetPlanosMock();

            _planoRepositoryMock.Setup(x => x.Get())
                .ReturnsAsync(planos);

            //Act
            var result = await planoService.ObterTodos();

            //Assert
            result.Should().Equal(planos);
        }

        private static Plano GetPlanoMock()
        {
            return new Plano
            {
                CodigoPlano = 25,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Enfermaria,
                Descricao = "Plano básico",
                TipoPlano = Enums.ETipoPlano.Individual
            };
        }

        private static List<Plano> GetPlanosMock()
        {
            return new List<Plano> {
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
