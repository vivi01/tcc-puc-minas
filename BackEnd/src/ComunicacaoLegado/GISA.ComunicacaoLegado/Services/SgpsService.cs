using GISA.EventBusRabbitMQ.Enums;
using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;
using System.Collections.Generic;

namespace GISA.ComunicacaoLegado.Services
{
    public class SgpsService : ISgpsService
    {
        public AutorizacaoExameResponse AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            //Nesta parte deverá ser implementado uma chamada para o serviço legado
            //retornar o status. 
            //Valor mocado para testes.]
            return new AutorizacaoExameResponse
            {
                Status = EStatusSolicitacao.Autorizado,
                Sucess = true,
                Title = "Autorizado pelo SGPS",
                DataAutorizacao = System.DateTime.Now,
                Errors = new List<string>()
            };
        }
    }
}
