using FluentAssertions;
using GISA.OcelotApiGateway.Controllers;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Net;
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

        [Test]
        public async Task GetAuthenticationDeveRetornarBadRequest()
        {
            //Arrange
            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _usuarioServiceMock.Setup(x => x.ValidarUsuario(usuario))
                .Returns(false);

            //Act
            var actionResult = await authController.GetAuthentication(usuario);

            //Assert
            var result = actionResult.Result as BadRequestObjectResult;
            Assert.AreEqual(Convert.ToInt32(HttpStatusCode.BadRequest), result.StatusCode);
        }

        [Test]
        public async Task GetAuthenticationDeveGerarToken()
        {
            //Arrange
            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            AuthToken oldToken = null;

            var token = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 03, 15),
                Token = "r45tyyu54545asçpowm",
                UserName = usuario.Username
            };


            _usuarioServiceMock.Setup(x => x.ValidarUsuario(usuario))
                .Returns(true);

            _tokenServiceMock.Setup(x => x.GetTokenByUserName(usuario.Username))
                .ReturnsAsync(oldToken);

            _tokenServiceMock.Setup(_ => _.GerarNovoToken(usuario))
                .ReturnsAsync(token);

            //Act
            var actionResult = await authController.GetAuthentication(usuario);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            result.Should().NotBeNull();
            result.Value.Should().Be(token);
        }

        [Test]
        public async Task GetUsuarioDeveRetornarAuthToken()
        {
            //Arrange
            var token = "r45tyyu54545asçpowm";

            var authToken = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 03, 15),
                Token = "r45tyyu54545asçpowm",
                UserName = "user"
            };

            _tokenServiceMock.Setup(x => x.GetTokenByToken(token))
                .ReturnsAsync(authToken);

            //Act
            var actionResult = await authController.GetUsuario(token);

            //Assert
            var result = actionResult.Result as OkObjectResult;
            Assert.AreEqual(Convert.ToInt32(HttpStatusCode.OK), result.StatusCode);
            result.Value.Should().Be(authToken);
        }
    }
}