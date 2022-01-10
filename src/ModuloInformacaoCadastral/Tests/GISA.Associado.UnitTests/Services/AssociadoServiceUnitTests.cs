using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.UnitTests.Services
{
    public class AssociadoServiceUnitTests
    {
        private AssociadoService associadoService;
        private Mock<IAssociadoRepository> _associadoRepositoryMock;
        private Mock<IPlanoService> _planoServiceMock;
        private Mock<IBus> _busControlMock;

        [SetUp]
        public void Setup()
        {
            _associadoRepositoryMock = new Mock<IAssociadoRepository>();
            _planoServiceMock = new Mock<IPlanoService>();
            _busControlMock = new Mock<IBus>();

            associadoService = new AssociadoService(_associadoRepositoryMock.Object,
                _busControlMock.Object, _planoServiceMock.Object);
        }

        [TestCase(ESituacaoAssociado.Ativo)]
        [TestCase(ESituacaoAssociado.Inativo)]
        [TestCase(ESituacaoAssociado.Suspenso)]
        public async Task GetSituacaoAssociadoComSucesso(ESituacaoAssociado situacaoAssociado)
        {
            //Arrange
            var codigoAssociado = 1257;
            _associadoRepositoryMock.Setup(x => x.GetSituacao(codigoAssociado))
                .ReturnsAsync(situacaoAssociado);

            //Act
            var result = await associadoService.GetSituacaoAssociado(codigoAssociado);

            //Assert           
            result.Equals(situacaoAssociado);
        }

        [Test]
        public async Task GetAssociadoByCodigoComSucesso()
        {
            //Arrange
            var codigoAssociado = 1257;
            var associado = GetMockAssociado(codigoAssociado);

            _associadoRepositoryMock.Setup(x => x.GetAssociado(codigoAssociado))
                .ReturnsAsync(associado);

            //Act
            var result = await associadoService.GetAssociadoByCodigo(codigoAssociado);

            //Assert           
            result.Equals(associado);
        }

        [Test]
        public async Task GetAssociadoByUserNameComSucesso()
        {
            //Arrange
            var userName = "user1";
            var associado = GetMockAssociado();

            _associadoRepositoryMock.Setup(x => x.GetAssociadoByUserName(userName))
                .ReturnsAsync(associado);

            //Act
            var result = await associadoService.GetAssociadoByUserName(userName);

            //Assert           
            result.Equals(associado);
        }

        [Test]
        public async Task GetValorPlanoComSucesso()
        {
            //Arrange
            var valor = 700;

            _associadoRepositoryMock.Setup(x => x.GetValorPlano())
                .ReturnsAsync(valor);

            //Act
            var result = await associadoService.GetValorPlano();

            //Assert           
            result.Equals(valor);
        }

        [Test]
        public async Task SolicitarMarcacaoExameComSucesso()
        {
            //Arrange
            var autorizacaoExame = new AutorizacaoExameMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = 1258,
                CodigoExame = 254,
                CodigoPlano = 27,
                DataExame = new System.DateTime(2022, 02, 10),
                MensagensErro = "",
                Status = "Autorizado",
                Token = "x14589909mlpq09875cv12"
            };

            var associado = GetMockAssociado();

            _busControlMock.Setup(_ => _.SendAsync(EventBusConstants.PrestadorExchange, autorizacaoExame))
                .Returns(Task.FromResult(autorizacaoExame));

            _associadoRepositoryMock.Setup(x => x.GetAssociado(autorizacaoExame.CodigoAssociado))
               .ReturnsAsync(associado);

            _associadoRepositoryMock.Setup(x => x.SalvarMarcacaoExame(associado));

            //Act
            var result = await associadoService.SolicitarMarcacaoExame(autorizacaoExame);

            //Assert           
            result.Equals("Marcação realizada com Sucesso!");
        }

        [TestCase("", false)]
        [TestCase("user1", true)]
        public async Task AlterarPlanoComSucesso(string userName, bool resultado)
        {
            //Arrange
            var token = "x14589909mlpq09875cv12";
            var codigoNovoPlano = 28;

            var plano = new Plano
            {
                CodigoPlano = 28,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Apartamento,
                Descricao = "Plano intermediário",
                TipoPlano = ETipoPlano.Empresarial
            };

            var request = new AuthTokenMsg(token)
            {
                UserName = userName
            };

            var associado = GetMockAssociado();

            _busControlMock.Setup(_ => _.SendAsync(EventBusConstants.AutenticacaoExchange, request))
                .Returns(Task.FromResult(request));

            _associadoRepositoryMock.Setup(x => x.GetAssociadoByUserName(userName))
               .ReturnsAsync(associado);

            _associadoRepositoryMock.Setup(x => x.Update(associado))
               .ReturnsAsync(resultado);

            _planoServiceMock.Setup(x => x.ObterPlanoPorCodigo(codigoNovoPlano))
                 .ReturnsAsync(plano);

            //Act
            var result = await associadoService.AlterarPlano(token, codigoNovoPlano, true);

            //Assert           
            result.Equals(resultado);
        }

        private static Entities.Associado GetMockAssociado(int codigoAssociado = 1234)
        {
            var endereco = new Endereco
            {
                Rua = "Rua X",
                CEP = "42510698",
                Cidade = "Rio de Janeiro",
                Estado = "Rio de Janeiro"
            };

            var plano = new Plano
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
                MarcacaoExames = new List<MarcacaoExame>()
            };
            return associado;
        }
    }
}
