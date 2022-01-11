using FluentAssertions;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.UnitTests.Services
{
    public class UsuarioServiceUnitTests
    {
        private UsuarioService usuarioService;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;

        [SetUp]
        public void SetUp()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();

            usuarioService = new UsuarioService(_usuarioRepositoryMock.Object);
        }

        [Test]
        public async Task GetUsuarioByIdDeveRetornarUsuario()
        {
            //Arrange
            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };
            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioById("associadoUser"))
                .ReturnsAsync(usuario);

            //Act
            var result = await usuarioService.GetUsuarioById("associadoUser");

            //Assert
            result.Should().Be(usuario);
        }

        [Test]
        public async Task GetUsuarioByUserNameDeveRetornarUsuario()
        {
            //Arrange
            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };
            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioByName("user1"))
                .ReturnsAsync(usuario);

            //Act
            var result = await usuarioService.GetUsuarioByUserName("user1");

            //Assert
            result.Should().Be(usuario);
        }

        [Test]
        public void ValidarUsuarioRetornarFalseQuandoUsuarioNaoExiste()
        {
            //Arrange
            AuthUser usuarioRetorno = null;

            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioByName("user1"))
                .ReturnsAsync(usuarioRetorno);

            //Act
            var result = usuarioService.ValidarUsuario(usuario);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidarUsuarioRetornarFalseQuandoSenhaEstaIncorreta()
        {
            //Arrange
            AuthUser usuarioRetorno = new()
            {
                Id = "associadoUser",
                Password = "teste@758",
                Role = "associado",
                Username = "user1"

            };

            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioByName("user1"))
                .ReturnsAsync(usuarioRetorno);

            //Act
            var result = usuarioService.ValidarUsuario(usuario);

            //Assert
            result.Should().BeFalse();
        }

        [Test]
        public void ValidarUsuarioRetornarTrue()
        {
            //Arrange
            AuthUser usuarioRetorno = new()
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioByName("user1"))
                .ReturnsAsync(usuarioRetorno);

            //Act
            var result = usuarioService.ValidarUsuario(usuario);

            //Assert
            result.Should().BeTrue();
        }
    }
}
