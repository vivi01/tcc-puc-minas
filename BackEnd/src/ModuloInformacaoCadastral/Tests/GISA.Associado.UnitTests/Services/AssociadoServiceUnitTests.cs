using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
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
        private Mock<IMarcacaoExameService> _marcacaoExameService;
        private Mock<IMessageBus> _busMock;

        [SetUp]
        public void Setup()
        {
            _associadoRepositoryMock = new Mock<IAssociadoRepository>();
            _planoServiceMock = new Mock<IPlanoService>();
            _marcacaoExameService = new Mock<IMarcacaoExameService>();
            _busMock = new Mock<IMessageBus>();

            associadoService = new AssociadoService(_associadoRepositoryMock.Object, _planoServiceMock.Object,
                 _busMock.Object, _marcacaoExameService.Object);
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
            int codigoAssociado = 265;

            _associadoRepositoryMock.Setup(x => x.GetValorPlano(codigoAssociado))
                .ReturnsAsync(valor);

            //Act
            var result = await associadoService.GetValorPlano(codigoAssociado);

            //Assert           
            result.Equals(valor);
        }

        [Test]
        public async Task SolicitarMarcacaoExameComSucesso()
        {
            //Arrange
            var marcacaoExameRequest = new MarcacaoExameMsg
            {
                RequestId = new System.Guid(),
                CodigoAssociado = 1258,
                CodigoExame = 254,
                CodigoPlano = 27,
                DataExame = new System.DateTime(2022, 02, 10),
                MensagensErro = "",
                Status = EStatusSolicitacao.Indefinido
            };

            var response = new AutorizacaoExameResponse
            {
                Status = EStatusSolicitacao.Autorizado,
                Sucess = true,
                Title = "Autorizado pelo SGPS",
                DataAutorizacao = System.DateTime.Now,
                Errors = new Dictionary<string, string[]>()
        };

            var marcacaoExameResponse = new MarcacaoExameResponse
            {
                DataAutorizacao = response.DataAutorizacao,
                CodigoExame = marcacaoExameRequest.CodigoExame,
                Situacao = response.Status,
                DataExame = marcacaoExameRequest.DataExame,
                MensagemErro = ""
            };

            var associado = GetMockAssociado();

            List<Entities.Associado> associadosList = new()
            {
                associado
            };

            var marcacaoExame = new MarcacaoExame
            {
                DataExame = marcacaoExameRequest.DataExame,
                CodigoExame = marcacaoExameRequest.CodigoExame,
                Associados = associadosList
            };

            _associadoRepositoryMock.Setup(x => x.GetAssociado(marcacaoExameRequest.CodigoAssociado))
               .ReturnsAsync(associado);

            _marcacaoExameService.Setup(x => x.Adicionar(marcacaoExame))
                .Returns(Task.FromResult(true));

            _busMock.Setup(_ => _.RequestAsync<MarcacaoExameMsg, AutorizacaoExameResponse>(marcacaoExameRequest))
                .Returns(Task.FromResult(response));

            //Act
            var result = await associadoService.SolicitarMarcacaoExame(marcacaoExameRequest);

            //Assert           
            result.Equals(marcacaoExameResponse);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task AlterarPlanoComSucesso(bool resultado)
        {
            //Arrange
            var codigoAssociado = 1258;
            var codigoNovoPlano = 28;

            var plano = new Plano
            {
                Id = 25,
                CodigoPlano = 28,
                ClassificacaoPlano = Enums.EClassificacaoPlano.Apartamento,
                Descricao = "Plano intermediário",
                TipoPlano = ETipoPlano.Empresarial
            };

            var associado = GetMockAssociado(codigoAssociado);

            _associadoRepositoryMock.Setup(x => x.GetAssociado(codigoAssociado))
              .ReturnsAsync(associado);

            _associadoRepositoryMock.Setup(x => x.Update(associado))
               .ReturnsAsync(resultado);

            _planoServiceMock.Setup(x => x.ObterPlanoPorCodigo(codigoNovoPlano))
                 .ReturnsAsync(plano);

            var alterarPlano = new AlterarPlano
            {
                CodigoAssociado = codigoAssociado,
                CodigoNovoPlano = codigoNovoPlano,
                PlanoOdontologico = true
            };

            //Act
            var result = await associadoService.AlterarPlano(alterarPlano);

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
