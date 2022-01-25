using GISA.Associado.Entities;
using GISA.Associado.Enums;
using GISA.Associado.Repositories.Interfaces;
using GISA.Associado.Services.Interfaces;
using GISA.EventBusRabbitMQ.Messages;
using System;
using System.Threading.Tasks;

namespace GISA.Associado.Services
{
    public class AssociadoService : IAssociadoService
    {
        private readonly IAssociadoRepository _associadoRepository;
        private readonly IPlanoService _planoService;

        public AssociadoService(IAssociadoRepository associadoRepository, IPlanoService planoService)
        {
            _associadoRepository = associadoRepository;
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
            await GetAssociadoByCodigo(requestMessage.CodigoAssociado);
        }

        public async Task<string> SolicitarMarcacaoExame(AutorizacaoExame autorizacaoExameMsg)
        {
            var associado = await GetAssociadoByCodigo(autorizacaoExameMsg.CodigoAssociado);

            if (associado == null)
                return "Associado Não Encontrado";

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

        public async Task<bool> AlterarPlano(AlterarPlano alterarPlano)
        {
            var usuario = await GetAssociadoByCodigo(alterarPlano.CodigoAssociado);

            var plano = await _planoService.ObterPlanoPorCodigo(alterarPlano.CodigoNovoPlano);

            usuario.PlanoId = plano.Id;
            usuario.PossuiPlanoOdontologico = alterarPlano.PlanoOdontologico;
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
