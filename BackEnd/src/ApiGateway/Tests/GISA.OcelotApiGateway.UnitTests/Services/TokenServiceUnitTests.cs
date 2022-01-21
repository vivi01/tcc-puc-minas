using FluentAssertions;
using GISA.OcelotApiGateway.Repositories.Interfaces;
using GISA.OcelotApiGateway.SecurityModel;
using GISA.OcelotApiGateway.Services;
using GISA.OcelotApiGateway.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace GISA.OcelotApiGateway.UnitTests.Services
{
    public class TokenServiceUnitTests
    {
        private TokenService _tokenService;
        private Mock<ITokenRepository> _tokenRepositoryMock;
        private Mock<ILogger<ITokenService>> _loggerMock;

        [SetUp]
        public void SetUp()
        {
            _tokenRepositoryMock = new Mock<ITokenRepository>();
            _loggerMock = new Mock<ILogger<ITokenService>>();

            _tokenService = new TokenService(_tokenRepositoryMock.Object, _loggerMock.Object);
        }

        [TestCase("associado")]
        [TestCase("conveniado")]
        [TestCase("prestador")]
        public async Task GerarNovoTokenDeveRetornarToken(string role)
        {
            //Arrange
            var authToken = new AuthToken();
            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _tokenRepositoryMock.Setup(x => x.Create(authToken))
                .Returns(Task.FromResult(authToken));

            //Act
            var result = await _tokenService.GerarNovoToken(usuario);

            //Assert
            result.Should().NotBeNull();
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals($"Gerado token para o usuário {usuario.Username} ", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
                Times.Once);
        }

        [Test]
        public async Task GetTokenByUserNameDeveRetornarAuthToken()
        {
            //Arrange
            var token = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 03, 15),
                Token = "r45tyyu54545asçpowm",
                UserName = "user1"
            };

            _tokenRepositoryMock.Setup(x => x.GetTokenByUserName("user1"))
                .Returns(Task.FromResult(token));

            //Act
            var result = await _tokenService.GetTokenByUserName("user1");

            //Assert
            result.Should().NotBeNull();
            result.Should().Be(token);
        }

        [Test]
        public async Task ValidarTokenDeveRetornarNovoAuthToken()
        {
            //Arrange
            var token = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 01, 10),
                Token = "r45tyyu54545asçpowm",
                UserName = "user1"
            };

            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _tokenRepositoryMock.Setup(x => x.GetTokenByUserName("user1"))
                .Returns(Task.FromResult(token));

            //Act
            var result = await _tokenService.ValidarToken(usuario, token);

            //Assert
            result.Should().NotBeNull();
            _loggerMock.Verify(
               x => x.Log(
                   LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((o, t) => string.Equals($"Token Expirado! Gerado novo token para o usuário {usuario.Username} ", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                   It.IsAny<Exception>(),
                   (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
               Times.Once);
        }

        [Test]
        public async Task ValidarTokenNaoDeveGerarNovoToken()
        {
            //Arrange
            var token = new AuthToken
            {
                ExpirationDate = new System.DateTime(2022, 08, 10).AddDays(2),
                Token = "r45tyyu54545asçpowm",
                UserName = "user1"
            };

            var usuario = new AuthUser
            {
                Password = "teste@658",
                Role = "associado",
                Username = "user1"

            };

            _tokenRepositoryMock.Setup(x => x.GetTokenByUserName("user1"))
                .Returns(Task.FromResult(token));

            //Act
            var result = await _tokenService.ValidarToken(usuario, token);

            //Assert
            result.Should().NotBeNull();
            _loggerMock.Verify(
               x => x.Log(
                   LogLevel.Information,
                   It.IsAny<EventId>(),
                   It.Is<It.IsAnyType>((o, t) => string.Equals($"Token Expirado! Gerado novo token para o usuário {usuario.Username} ", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                   It.IsAny<Exception>(),
                   (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()),
               Times.Never);
        }
    }
}
