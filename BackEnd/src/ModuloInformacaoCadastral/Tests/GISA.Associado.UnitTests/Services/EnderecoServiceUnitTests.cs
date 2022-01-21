using FluentAssertions;
using GISA.Associado.Entities;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public async Task AdicionarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Add(endereco))
                .ReturnsAsync(true);

            //Act
            var result = await enderecoService.Adicionar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task EditarDeveSerExecutadoComSucesso()
        {
            //Arrange
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Update(endereco))
                .ReturnsAsync(true);

            //Act
            var result = await enderecoService.Editar(endereco);

            //Assert
            result.Should().BeTrue();
        }

        [Test]
        public async Task ObterPorIdDeveSerExecutadoComSucesso()
        {
            //Arrange
            var enderecoId = 7;
            var endereco = GetEnderecoMock();

            _enderecoRepositoryMock.Setup(x => x.GetById(enderecoId))
                .ReturnsAsync(endereco);

            //Act
            var result = await enderecoService.ObterPorId(enderecoId);

            //Assert
            result.Should().Be(endereco);
        }

        [Test]
        public async Task DeletarDeveSerExecutadoComSucesso()
        {
            var endereco = GetEnderecoMock();
            _enderecoRepositoryMock.Setup(x => x.Delete(endereco)).ReturnsAsync(true);
            var result = await enderecoService.Deletar(endereco);
            result.Should().BeTrue();
        }

        [Test]
        public async Task ObterTodosDeveSerExecutadoComSucesso()
        {
            //Arrange
            var enderecos = GetEnderecosMock();

            _enderecoRepositoryMock.Setup(x => x.Get())
                .ReturnsAsync(enderecos);

            //Act
            var result = await enderecoService.ObterTodos();

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
