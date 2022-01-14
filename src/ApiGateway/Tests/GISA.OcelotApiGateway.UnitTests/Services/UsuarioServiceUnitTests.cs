using FluentAssertions;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services;
using GISA.OcelotApiGateway.Settings;
using Microsoft.AspNetCore.DataProtection;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.UnitTests.Services
{
    public class UsuarioServiceUnitTests
    {
        private UsuarioService usuarioService;
        private Mock<IUsuarioRepository> _usuarioRepositoryMock;
        private Mock<IDataProtectionProvider> _rootProvider;
        private IAuthDatabaseSettings _settings;
        private Mock<IDataProtector> _mockDataProtector;

        [SetUp]
        public void SetUp()
        {
            _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
            _rootProvider = new Mock<IDataProtectionProvider>();
            _mockDataProtector = new Mock<IDataProtector>();

            _settings = new AuthDatabaseSettings
            {
                DatabaseName = "AuthDb",
                UsuarioCollectionName = "Usuarios",
                KeyDataProvider = "Authentication.Class.v1"
            };

            usuarioService = new UsuarioService(_usuarioRepositoryMock.Object, _rootProvider.Object, _settings);
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
                Password = "CfDJ8Mz3BQjJZiJPhldze0wxC8v2LrVPvxA2CoLzAz2T1bXyjZpFlEVz6L9vXG1RVtz4weZrf2cYvNfrvbGZuDGq2GNIcRU4 - 5sGR9U9ZPBCAeP2qZgOL - VxbrBb2ijCBODhXQ",
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

            _rootProvider.Setup(_ => _.CreateProtector(_settings.KeyDataProvider))
                .Returns(_mockDataProtector.Object);

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
                Password = "CfDJ8Mz3BQjJZiJPhldze0wxC8v2LrVPvxA2CoLzAz2T1bXyjZpFlEVz6L9vXG1RVtz4weZrf2cYvNfrvbGZuDGq2GNIcRU4 - 5sGR9U9ZPBCAeP2qZgOL - VxbrBb2ijCBODhXQ",
                Role = "associado",
                Username = "user1"

            };

            var usuario = new AuthUser
            {
                Id = "associadoUser",
                Password = "",
                Role = "associado",
                Username = "user1"

            };

            _usuarioRepositoryMock.Setup(_ => _.GetUsuarioByName("user1"))
                .ReturnsAsync(usuarioRetorno);

            _rootProvider.Setup(_ => _.CreateProtector(_settings.KeyDataProvider))
               .Returns(_mockDataProtector.Object);

            //Act
            var result = usuarioService.ValidarUsuario(usuario);

            //Assert
            result.Should().BeTrue();
        }
    }
}
