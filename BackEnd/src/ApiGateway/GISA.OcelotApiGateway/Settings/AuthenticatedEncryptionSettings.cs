using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace GISA.OcelotApiGateway.Settings
{
    public class AuthenticatedEncryptionSettings
    {
        public EncryptionAlgorithm EncryptionAlgorithm { get; set; }
        public ValidationAlgorithm ValidationAlgorithm { get; set; }
    }
}
