using FluentAssertions;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using GISA.Associado.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

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
        public void AdicionarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Add(endereco))
                .Returns(true);

            //Act
            var result = planoService.Adicionar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void EditarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Update(endereco))
                .Returns(true);

            //Act
            var result = planoService.Editar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ObterPorIdDeveSerExecutadoComSucesso()
        {
            //Arrange
            var planoId = 25;
            var plano = GetPlanoMock();

            _planoRepositoryMock.Setup(x => x.GetById(planoId))
                .Returns(plano);

            //Act
            var result = planoService.ObterPlanoPorCodigo(planoId);

            //Assert
            result.Should().Be(plano);
        }

        [Test]
        public void DeletarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetPlanoMock();
            _planoRepositoryMock.Setup(x => x.Delete(endereco))
                .Returns(true);

            //Act
            var result = planoService.Deletar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ObterTodosDeveSerExecutadoComSucesso()
        {
            //Arrange
            var planos = GetEnderecosMock().AsQueryable();

            _planoRepositoryMock.Setup(x => x.Get())
                .Returns(planos);

            //Act
            var result = planoService.ObterTodos();

            //Assert
            result.Result.Should().Equal(planos.ToList());
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

        private static List<Plano> GetEnderecosMock()
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
