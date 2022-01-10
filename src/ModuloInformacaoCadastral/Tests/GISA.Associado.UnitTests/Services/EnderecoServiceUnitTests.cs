using FluentAssertions;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace GISA.Associado.UnitTests.Services
{
    public class EnderecoServiceUnitTests
    {
        private EnderecoService enderecoService;
        private Mock<IEnderecoRepository> _enderecoRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _enderecoRepositoryMock = new Mock<IEnderecoRepository>();
            enderecoService = new EnderecoService(_enderecoRepositoryMock.Object);
        }

        [Test]
        public void AdicionarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Add(endereco))
                .Returns(true);

            //Act
            var result = enderecoService.Adicionar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void EditarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Update(endereco))
                .Returns(true);

            //Act
            var result = enderecoService.Editar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ObterPorIdDeveSerExecutadoComSucesso()
        {
            //Arrange
            var enderecoId = 7;
            var endereco = GetEnderecoMock();

            _enderecoRepositoryMock.Setup(x => x.GetById(enderecoId))
                .Returns(endereco);

            //Act
            var result = enderecoService.ObterPorId(enderecoId);

            //Assert
            result.Should().Be(endereco);
        }

        [Test]
        public void DeletarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Delete(endereco))
                .Returns(true);

            //Act
            var result = enderecoService.Deletar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public void ObterTodosDeveSerExecutadoComSucesso()
        {
            //Arrange
            var enderecos = GetEnderecosMock().AsQueryable();

            _enderecoRepositoryMock.Setup(x => x.Get())
                .Returns(enderecos);

            //Act
            var result = enderecoService.ObterTodos();

            //Assert
            result.Should().Equal(enderecos.ToList());
        }

        private static Endereco GetEnderecoMock()
        {
            return new Endereco
            {
                CEP = "40265310",
                Cidade = "Fortaleza",
                Estado = "Ceará",
                Rua = "Rua XWZ"
            };
        }

        private static List<Endereco> GetEnderecosMock()
        {
            return new List<Endereco> {
                new Endereco
                {
                    CEP = "40265310",
                    Cidade = "Fortaleza",
                    Estado = "Ceará",
                    Rua = "Rua XWZ"
                },
                new Endereco
                {
                    CEP = "40855319",
                    Cidade = "Salvador",
                    Estado = "Bahia",
                    Rua = "Rua Teste"
                },
            };
        }
    }
}
