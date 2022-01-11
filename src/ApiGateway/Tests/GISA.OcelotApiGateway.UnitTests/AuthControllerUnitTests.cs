using FluentAssertions;
using GISA.OcelotApiGateway.Controllers;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.UnitTests
{
    public class AuthControllerUnitTests
    {
        private AuthController authController;
        private Mock<ITokenService> _tokenServiceMock;
        private Mock<IUsuarioService> _usuarioServiceMock;

        [SetUp]
        public void Setup()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _usuarioServiceMock = new Mock<IUsuarioService>();

            authController = new AuthController(_tokenServiceMock.Object, _usuarioServiceMock.Object);
        }

        [Test]
        public async Task GetAuthenticationDeveRetornarToken()
        {
            //Arrange
            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            var token = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 03, 15),
                Token = "r45tyyu54545asçpowm",
                UserName = usuario.Username
            };


            _usuarioServiceMock.Setup(x => x.ValidarUsuario(usuario))
                .Returns(true);

            _tokenServiceMock.Setup(x => x.GetTokenByUserName(usuario.Username))
                .ReturnsAsync(token);

            _tokenServiceMock.Setup(_ => _.ValidarToken(usuario, token))
                .ReturnsAsync(token);

            //Act
            var actionResult = await authController.GetAuthentication(usuario);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(token);
        }
    }
}