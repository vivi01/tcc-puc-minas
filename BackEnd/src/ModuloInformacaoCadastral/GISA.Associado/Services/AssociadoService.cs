using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository;
        private readonly IPlanoService _planoService;
        private readonly IMessageBus _bus;
        private readonly IMarcacaoExameService _marcacaoExameService;
        public AssociadoService(IAssociadoRepository associadoRepository, IPlanoService planoService,
            IMessageBus bus, IMarcacaoExameService marcacaoExameService)
        {
            _associadoRepository = associadoRepository;
            _planoService = planoService;
            _bus = bus;
            _marcacaoExameService = marcacaoExameService;
        }

        public Task<ESituacaoAssociado> GetSituacaoAssociado(int codigoAssociado)
        {
            return _associadoRepository.GetSituacao(codigoAssociado);
        }

        public Task<Entities.Associado> GetAssociadoByCodigo(int codigoAssociado)
        {
            return _associadoRepository.GetAssociado(codigoAssociado);
        }

        public Task<bool> CadastrarAssociado(Entities.Associado associado)
        {
            return _associadoRepository.Add(associado);
        }

        public Task<decimal> GetValorPlano(int codigoAssociado)
        {
            return _associadoRepository.GetValorPlano(codigoAssociado);
        }

        public Task<Entities.Associado> GetAssociadoByUserName(string userName)
        {
            return _associadoRepository.GetAssociadoByUserName(userName);
        }

        public async Task<MarcacaoExameResponse> SolicitarMarcacaoExame(MarcacaoExameMsg marcacaoExameRequest)
        {
            var associado = await GetAssociadoByCodigo(marcacaoExameRequest.CodigoAssociado);

            if (associado == null)
                return GetMarcacaoExameResponse(null, marcacaoExameRequest.DataExame, marcacaoExameRequest.CodigoExame,
                    EStatusSolicitacao.Erro, "Usuário não encontrado");

            //chama o serviço do prestador para solicitar a marcação
            var marcacaoResponse = await SolicitarMarcacao(marcacaoExameRequest);

            if (marcacaoResponse.Status != EStatusSolicitacao.Autorizado)
                return GetMarcacaoExameResponse(null, marcacaoExameRequest.DataExame, marcacaoExameRequest.CodigoExame,
                    marcacaoResponse.Status, marcacaoResponse.Errors.ToString());

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

            associado.MarcacaoExames.Add(marcacaoExame);

            var result = await _marcacaoExameService.Adicionar(marcacaoExame);

            if (!result)
                return GetMarcacaoExameResponse(null, marcacaoExameRequest.DataExame, marcacaoExameRequest.CodigoExame,
                   EStatusSolicitacao.Erro, "Erro ao tentar salvar marcação!");

            return GetMarcacaoExameResponse(marcacaoResponse.DataAutorizacao, marcacaoExameRequest.DataExame,
                marcacaoExameRequest.CodigoExame, marcacaoResponse.Status, marcacaoResponse.Errors.ToString());
        }

        public async Task<bool> AlterarPlano(AlterarPlano alterarPlano)
        {
            var usuario = await GetAssociadoByCodigo(alterarPlano.CodigoAssociado);

            var plano = await _planoService.ObterPlanoPorCodigo(alterarPlano.CodigoNovoPlano);

            usuario.PlanoId = plano.Id;
            usuario.PossuiPlanoOdontologico = alterarPlano.PlanoOdontologico;
            usuario.ValorPlano = CalcularValorNovoPlano(usuario.DataNascimento, plano);

            return await _associadoRepository.Update(usuario);
        }

        private async Task<AutorizacaoExameResponse> SolicitarMarcacao(MarcacaoExameMsg marcacaoExameRequest)
        {
            return await _bus.RequestAsync<MarcacaoExameMsg, AutorizacaoExameResponse>(marcacaoExameRequest);
        }

        private static MarcacaoExameResponse GetMarcacaoExameResponse(DateTime? dataAutorizacao, DateTime dataExame,
            int codigoExame, EStatusSolicitacao situacao, string erro)
        {
            return new MarcacaoExameResponse
            {
                DataAutorizacao = dataAutorizacao,
                CodigoExame = codigoExame,
                Situacao = situacao,
                DataExame = dataExame,
                MensagemErro = erro
            };
        }

        private static decimal CalcularValorNovoPlano(DateTime dataNascimento, Plano plano)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;

            if (plano.TipoPlano == ETipoPlano.Empresarial)
                return CalcularValorPlanoEmpresarial(idade, plano.ValorBase, plano.ClassificacaoPlano);

            return CalcularValorPlanoIndividual(idade, plano.ValorBase, plano.ClassificacaoPlano); ;
        }

        private static decimal CalcularValorPlanoEmpresarial(int idade, decimal valorPlanoBase, EClassificacaoPlano classificacaoPlano)
        {
            if (idade < 18)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 30;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 100;

                }
            }

            if (idade > 18 && idade <= 23)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 70;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 150;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 120;

                }
            }

            if (idade > 23 && idade <= 28)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 82;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 125;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 175;

                }
            }

            if (idade >= 29 && idade <= 33)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 92;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 175;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 245;

                }
            }

            if (idade >= 34 && idade <= 50)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 100;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 195;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 273;

                }
            }

            switch (classificacaoPlano)
            {
                case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 112.50M;
                case EClassificacaoPlano.Apartamento: return valorPlanoBase += 225;
                case EClassificacaoPlano.Vip: return valorPlanoBase += 302;
                default:
                    break;
            }

            return 0;
        }

        private static decimal CalcularValorPlanoIndividual(int idade, decimal valorPlanoBase, EClassificacaoPlano classificacaoPlano)
        {
            if (idade < 18)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 100;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 200;

                }
            }

            if (idade > 18 && idade <= 23)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 50;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 100;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 200;

                }
            }

            if (idade > 23 && idade <= 28)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 60;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 105;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 215;

                }
            }

            if (idade >= 29 && idade <= 33)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 72;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 125;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 245;

                }
            }

            if (idade >= 34 && idade <= 50)
            {
                switch (classificacaoPlano)
                {
                    case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 90.50M;
                    case EClassificacaoPlano.Apartamento: return valorPlanoBase += 165;
                    case EClassificacaoPlano.Vip: return valorPlanoBase += 292;

                }
            }

            switch (classificacaoPlano)
            {
                case EClassificacaoPlano.Enfermaria: return valorPlanoBase += 102.50M;
                case EClassificacaoPlano.Apartamento: return valorPlanoBase += 265;
                case EClassificacaoPlano.Vip: return valorPlanoBase += 352;

            }

            return 0;
        }
    }
}
