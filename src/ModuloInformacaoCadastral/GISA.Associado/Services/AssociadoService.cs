using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Common;
using GISA.EventBusRabbitMQ.Events;
using GISA.EventBusRabbitMQ.Interfaces;
using GISA.EventBusRabbitMQ.ModeloMensagens;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository;
        private readonly IPlanoService _planoService;
        private readonly IBus _busControl;

        public AssociadoService(IAssociadoRepository associadoRepository, IBus busControl, IPlanoService planoService)
        {
            _associadoRepository = associadoRepository;
            _busControl = busControl;
            _planoService = planoService;
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

        public Task<decimal> GetValorPlano()
        {
            return _associadoRepository.GetValorPlano();
        }

        public Task<Entities.Associado> GetAssociadoByUserName(string userName)
        {
            return _associadoRepository.GetAssociadoByUserName(userName);
        }

        public async Task GetSituacaoAssociado(AssociadoMsg requestMessage)
        {
            var situacao = await GetAssociadoByCodigo(requestMessage.CodigoAssociado);

            await _busControl.ReceiveAsync<AssociadoMsg>(EventBusConstants.GisaQueue,
                     x =>
                     {
                         requestMessage.Status = situacao.SituacaoAssociado.ToString();
                     });
        }

        public async Task<string> SolicitarMarcacaoExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            var associado = await GetAssociadoByCodigo(autorizacaoExameMsg.CodigoAssociado);

            if (associado == null)
                return "Associado Não Encontrado";

            await _busControl.SendAsync<AutorizacaoExameMsg>(EventBusConstants.GisaQueue, autorizacaoExameMsg);

            if (autorizacaoExameMsg.Status != "Autorizado")
                return autorizacaoExameMsg.MensagensErro;

            associado.MarcacaoExames.Add(new MarcacaoExame
            {
                DataExame = autorizacaoExameMsg.DataExame,
                CodigoExame = autorizacaoExameMsg.CodigoExame
            });

            await _associadoRepository.SalvarMarcacaoExame(associado);

            return "Marcação realizada com Sucesso!";
        }

        public async Task<bool> AlterarPlano(string token, int codigoNovoPlano, bool planoOdontologico)
        {
            var request = new AuthTokenMsg(token);

            await _busControl.SendAsync<AuthTokenMsg>(EventBusConstants.AutenticacaoExchange, request);

            if (string.IsNullOrWhiteSpace(request.UserName))
                return false;

            var usuario = await GetAssociadoByUserName(request.UserName);

            var plano = await _planoService.ObterPlanoPorCodigo(codigoNovoPlano);

            usuario.Plano = plano;
            usuario.PossuiPlanoOdontologico = planoOdontologico;
            usuario.ValorPlano = CalcularValorNovoPlano(usuario.DataNascimento, plano);

            return await _associadoRepository.Update(usuario);
        }

        private decimal CalcularValorNovoPlano(DateTime dataNascimento, Plano plano)
        {
            var idade = DateTime.Now.Year - dataNascimento.Year;

            if (plano.TipoPlano == ETipoPlano.Empresarial)
                return CalcularValorPlanoEmpresarial(idade, plano.ValorBase, plano.ClassificacaoPlano);

            return CalcularValorPlanoIndividual(idade, plano.ValorBase, plano.ClassificacaoPlano); ;
        }

        private decimal CalcularValorPlanoEmpresarial(int idade, decimal valorPlanoBase,
            EClassificacaoPlano classificacaoPlano)
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

            }

            return 0;
        }

        private decimal CalcularValorPlanoIndividual(int idade, decimal valorPlanoBase,
            EClassificacaoPlano classificacaoPlano)
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
