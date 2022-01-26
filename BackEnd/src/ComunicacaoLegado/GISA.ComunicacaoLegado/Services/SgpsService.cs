using GISA.EventBusRabbitMQ.Messages;
using GISA.EventBusRabbitMQ.Messages.Integracao;

namespace GISA.ComunicacaoLegado.Services
{
    public class SgpsService : ISgpsService
    {
        public DefaultResponse AutorizarExame(AutorizacaoExameMsg autorizacaoExameMsg)
        {
            //Nesta parte deverá ser implementado uma chamada para o serviço legado
            //retornar o status. 
            //Valor mocado para testes.]
            return new DefaultResponse
            {
                Status = "Autorizado",
                Sucess = true,
                Title = "Autorizado pelo SGPS"
            };
        }
    }
}
