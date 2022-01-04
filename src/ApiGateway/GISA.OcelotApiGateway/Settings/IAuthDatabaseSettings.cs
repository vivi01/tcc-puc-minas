namespace GISA.OcelotApiGateway.Settings
{
    public interface IAuthDatabaseSettings
    {
        string ConnectionString { get; set; }

        string DatabaseName { get; set; }

        string UsuarioCollectionName { get; set; }

        string TokenCollectionName { get; set; }
    }
}
