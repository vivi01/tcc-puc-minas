namespace GISA.OcelotApiGateway.Settings
{
    public class AuthDatabaseSettings : IAuthDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string UsuarioCollectionName { get; set; }
        public string TokenCollectionName { get; set; }
        public string KeyDataProvider { get; set; }
    }
}
