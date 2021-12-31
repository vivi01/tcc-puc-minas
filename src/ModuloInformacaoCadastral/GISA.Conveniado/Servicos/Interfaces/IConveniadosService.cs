namespace GISA.Conveniado.Servicos.Interfaces
{
    public interface IConveniadosService
    {
        string SolicitarAutorizacoExame(string token, int codigoExame, int codigoAssociado);
    }
}
